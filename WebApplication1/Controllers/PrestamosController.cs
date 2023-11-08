using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PrestamosController : Controller
    {
        // GET: Prestamos
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {
                var hoy = DateTime.Now;
                var prestamosActivos = context.Prestamos.Where(p => p.FechaEntrega == null).ToList();
                return View(prestamosActivos);
            }
        }

        public ActionResult Inactivos()
        {
            using (DbModels context = new DbModels())
            {
                var hoy = DateTime.Now;
                var prestamosInactivos = context.Prestamos.Where(p => p.FechaEntrega != null && p.FechaCompromisoDevolucion < hoy).ToList();
                return View(prestamosInactivos);
            }
        }

        public ActionResult Atrasados()
        {
            using (DbModels context = new DbModels())
            {
                var hoy = DateTime.Now;
                var prestamosAtrasados = context.Prestamos.Where(p => p.FechaEntrega == null && p.FechaCompromisoDevolucion < hoy).ToList();
                return View(prestamosAtrasados);
            }
        }

        // GET: Prestamos/Details/5
        public ActionResult Details(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Prestamos.FirstOrDefault(x => x.ID == id));
            }
        }

        // GET: Prestamos/Create
        public ActionResult Create()
        {
            using (DbModels context = new DbModels())
            {
                var colaboradoresActivos = context.Colaboradores
                    .Where(c => c.Estado == "a")
                    .Where(c => c.Prestamos.Count(p => p.FechaEntrega == null) <= 4)
                    .ToList();
                var HerramientasDisponibles = context.Herramientas.Where(c => c.CantidadDisponible > 0).ToList();

                ViewBag.CedulaColaborador = new SelectList(colaboradoresActivos, "CedulaIdentidad", "CedulaIdentidad");
                ViewBag.Herramientas = new SelectList(HerramientasDisponibles, "CodigoBarra", "CodigoBarra");
            }
            return View();
        }

        // POST: Prestamos/Create
        [HttpPost]
        public ActionResult Create(Prestamos prestamos)
        {
            using (DbModels context = new DbModels())
            {
                DateTime fechaCompromiso = prestamos.FechaCompromisoDevolucion;
                DateTime hoy = DateTime.Now;
                TimeSpan diferencia = fechaCompromiso - hoy;

                if (diferencia.TotalDays <= 5 & diferencia.TotalDays >= 1)
                {
                    var herramienta = context.Herramientas.FirstOrDefault(h => h.CodigoBarra == prestamos.CodigoHerramienta);
                    herramienta.CantidadDisponible--;
                    context.Prestamos.Add(prestamos);
                    prestamos.FechaPrestamo = DateTime.Now;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "La fecha de compromiso de devolución debe ser dentro de los próximos 5 días.";
                    return View();
                }
            }
        }

        // GET: Prestamos/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Prestamos.Where(x => x.ID == id).FirstOrDefault());
            }
        }

        // POST: Prestamos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Prestamos prestamos)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(prestamos).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Prestamos/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Prestamos.Where(x => x.ID == id).FirstOrDefault());
            }
        }

        // POST: Prestamos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    Prestamos prestamos = context.Prestamos.Where(x => x.ID == id).FirstOrDefault();
                    var herramienta = context.Herramientas.FirstOrDefault(h => h.CodigoBarra == prestamos.CodigoHerramienta);
                    herramienta.CantidadDisponible++;
                    prestamos.FechaEntrega = DateTime.Now;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

using System;
using System.Collections.Generic;
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
            return View();
        }

        // POST: Prestamos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Prestamos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Prestamos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        // POST: Prestamos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

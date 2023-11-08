using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ColaboradoresController : Controller
    {
        // GET: Colaboradores
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Colaboradores.ToList());
            }
        }

        // GET: Colaboradores/Details/5
        public ActionResult Details(string id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Colaboradores.FirstOrDefault(x => x.CedulaIdentidad == id));
            }
        }

        // GET: Colaboradores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Colaboradores/Create
        [HttpPost]
        public ActionResult Create(Colaboradores colaboradores)
        {
            try
            {
                // TODO: Add insert logic here
                using (DbModels context = new DbModels())
                {
                    context.Colaboradores.Add(colaboradores);
                    colaboradores.FechaRegistro = DateTime.Now;
                    colaboradores.Estado = "a";
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Colaboradores/Edit/5
        public ActionResult Edit(string id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Colaboradores.Where(x => x.CedulaIdentidad == id).FirstOrDefault());
            }
        }

        // POST: Colaboradores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Colaboradores colaboradores)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(colaboradores).State = EntityState.Modified;
                    context.Entry(colaboradores).Property(x => x.FechaRegistro).IsModified = false;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Colaboradores/Delete/5
        public ActionResult Delete(string id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Colaboradores.Where(x => x.CedulaIdentidad == id).FirstOrDefault());
            }
        }

        // POST: Colaboradores/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    Colaboradores colaboradores = context.Colaboradores.Where(x => x.CedulaIdentidad == id).FirstOrDefault();
                    context.Colaboradores.Remove(colaboradores);
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

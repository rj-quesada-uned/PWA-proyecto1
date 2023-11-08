using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HerramientasController : Controller
    {
        // GET: Herramientas
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Herramientas.ToList());
            }
        }

        // GET: Herramientas/Details/5
        public ActionResult Details(string id)
        {

            using (DbModels context = new DbModels())
            {
                return View(context.Herramientas.FirstOrDefault(x => x.CodigoBarra == id));
            }
        }

        // GET: Herramientas/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Herramientas/Create
        [HttpPost]
        public ActionResult Create(Herramientas herramientas)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Herramientas.Add(herramientas);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Herramientas/Edit/5
        public ActionResult Edit(string id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Herramientas.Where(x => x.CodigoBarra == id).FirstOrDefault());
            }
        }

        // POST: Herramientas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Herramientas herramientas)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(herramientas).State = EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Herramientas/Delete/5
        public ActionResult Delete(string id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Herramientas.Where(x => x.CodigoBarra == id).FirstOrDefault());
            }
        }

        // POST: Herramientas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    Herramientas herramientas = context.Herramientas.Where(x => x.CodigoBarra == id).FirstOrDefault();
                    context.Herramientas.Remove(herramientas);
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

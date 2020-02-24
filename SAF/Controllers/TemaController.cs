using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAF.DAL;
using SAF.Models;

namespace SAF.Controllers
{
    public class TemaController : Controller
    {
        TemaDAL conn = new TemaDAL();
        ClaseDAL connC = new ClaseDAL();

        // GET: Tema
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult temas()
        {
            var tema2 = new Tema();
           
            return View(tema2);
            
        }

        [HttpPost]
        public ActionResult temas(Tema form)
        {
            
            var id = Request["ipo[]"].Split(',');
            return View();
        }

        // GET: Tema/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tema/Create
        public ActionResult Create()
        {
            var clases = connC.cargar();
            ViewBag.lc = clases;
            return View();
        }

        // POST: Tema/Create
        [HttpPost]
        public ActionResult Create(Tema tema, FormCollection form)
        {
            try
            {
                // TODO: Add insert logic here
                int idClase = int.Parse(form["ddlClase"]);
                conn.agregar(idClase, tema);
                TempData["msg"] = "<script>alert('Tema agregado exitosamente')</script>";
                return RedirectToAction("Create");
            }
            catch
            {
                TempData["msg"] = "<script>alert('Tema no agregado')</script>";
                return View();
            }
        }

        // GET: Tema/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tema/Edit/5
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

        // GET: Tema/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tema/Delete/5
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

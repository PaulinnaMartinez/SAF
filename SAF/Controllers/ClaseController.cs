using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAF.DAL;
using SAF.Models;

namespace SAF.Controllers
{
    public class ClaseController : Controller
    {
        ClaseDAL conn = new ClaseDAL();

        // GET: Clase
        public ActionResult Index()
        {
            return View();
        }

        // GET: Clase/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Clase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clase/Create
        [HttpPost]
        public ActionResult Create(Clase clase)
        {
            try
            {
                conn.agregar(clase);
                TempData["msg"] = "<script>alert('Clase agregada exitosamente')</script>";
                return RedirectToAction("Create");
            }
            catch
            {
                TempData["msg"] = "<script>alert('Clase no agregada')</script>";
                return View();
            }
        }

        // GET: Clase/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Clase/Edit/5
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

        // GET: Clase/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Clase/Delete/5
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

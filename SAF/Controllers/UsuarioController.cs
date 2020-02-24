using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAF.DAL;
using SAF.Models;

namespace SAF.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioDAL conn = new UsuarioDAL();
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Details()
        {
            Usuario id = ((Usuario)Session["user"]);
            Usuario user = conn.cargarUsuario(id.matricula);
            return View(user);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                // TODO: Add insert logic here
                if (usuario.password == usuario.password2)
                {
                    int matricula = conn.agregar(usuario);
                    TempData["msg"] = "<script>alert('Tu matricula es " + matricula + " ')</script>";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Las contraseñas no coinciden ')</script>";
                    return RedirectToAction("Create", "Usuario");
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit()
        {
            Usuario id = ((Usuario)Session["user"]);
            Usuario user = conn.cargarUsuario(id.matricula);
            user.password2 = user.password;
            return View(user);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            try
            {
                // TODO: Add update logic here

                if (usuario.password == usuario.password2)
                {
                    conn.editar(usuario);
                    TempData["msg"] = "<script>alert('Usuario modificado correctamente');</script>";
                    return RedirectToAction("Edit");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Las contraseñas no coinciden, usuario no modificado');</script>";
                    return RedirectToAction("Edit");
                }

                
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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

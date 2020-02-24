using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAF.DAL;
using SAF.Models;


namespace SAF.Controllers
{
    public class HomeController : Controller
    {
        HomeDAL conn = new HomeDAL();

        // GET: Home
        public ActionResult Index()
        {
            
            return View();
        
        }

        public ActionResult inicio ( int matricula, string modalidad, string password)
        {
            Home login = new Home(matricula,password);
            Usuario user = conn.verificar(login);
            Session["user"] = user;
            Session["modo"] = modalidad;
            if (user!=null)
            {
                if (user.modalidad == "C" && modalidad == "M")
                {
                    TempData["alert"] = "Modalidad Solo alumno, favor de editar perfil";
                    TempData["cargado"] = "S";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Grupo");
            }
            else
            {
                
                TempData["alert"] = "Usuario/password incorrecto";
                TempData["cargado"] = "S";
                return RedirectToAction("Index");
            }
            
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAF.DAL;
using SAF.Models;

namespace SAF.Controllers
{
    public class GrupoController : Controller
    {
        int claseid = 2;
        GrupoDAL conn = new GrupoDAL();
        ClaseDAL con = new ClaseDAL();
        TemaDAL connt = new TemaDAL();
        Detalles d = new Detalles();
        DetallesDAL connd = new DetallesDAL();

        // GET: Grupo
        public ActionResult Index()
        {
            Usuario user = ((Usuario)Session["user"]);
            string modo = Session["modo"].ToString();
            List<Grupo> grupos = conn.cargar(user, modo);
            return View(grupos);
        }

        // GET: Grupo/Details/5
        public ActionResult Details(int idG, int idC, int mat)
        {
            d.modo = connd.cargarModalidad(mat);
            d.nombre = connd.cargarTema(idG);
            d.descripcion = connd.cargarDescripcion(idC);
            return View(d);
        }

        // GET: Grupo/Create
        public ActionResult Create()
        {
           
            var clase1 = con.cargar();
            ViewBag.lc = clase1;

            return View();
        }

        [HttpGet]
        public PartialViewResult temas(int id)
        {
            Grupo tema = new Grupo();
            tema.listTema = connt.cargar(id);
            return PartialView(tema);
        }


        // POST: Grupo/Create
        [HttpPost]
        public ActionResult Create(Grupo grupo, FormCollection form)
        {
            try
            {
                // TODO: Add insert logic here
                int idc = int.Parse(form["ddlClase"]);
                //var idt = Request["cbTema[]"].ToString().Join("','");
                var idt = Request["cbTema[]"].Split(',');
                List<int> idTemas = new List<int>();

                for (int i = 0; i < idt.Length; i++)
                {
                    int aux = int.Parse(idt[i]);
                    idTemas.Add(aux);
                }

                Usuario user = ((Usuario)Session["user"]);
                conn.agregar(grupo, user, idc, idTemas);
                //conn.agregarTema(idTemas);
                TempData["msg"] = "<script>alert('Grupo agregado')</script>";
                return RedirectToAction("Create");
            }
            catch (Exception e)
            {
                return RedirectToAction("Create");
            }
        }

        // GET: Grupo/Edit/5
        public ActionResult Edit(int id)
        {
            
            Grupo g = conn.cargarGrupo(id);
            return View(g);
        }

        // POST: Grupo/Edit/5
        [HttpPost]
        public ActionResult Edit(Grupo grupo)
        {

            try
            {
                
                conn.editar(grupo);
                TempData["msg"] = "<script>alert('Grupo editado exitosamente')</script>";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Grupo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Grupo/Delete/5
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

using Gustov.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gustov.Controllers
{
    public class HomeController : Controller
    {
        private gustovEntities db = new gustovEntities();

        MySql.Data.MySqlClient.MySqlConnection conn;
        string connection = ConfigurationManager.ConnectionStrings["GUSTOVConnection"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarMenu()
        {

            var listaMenu = (from p in db.platos

                             select new
                             {
                                 p.id_plato,
                                 p.nombre_plato,
                                 p.cantidad_plato
                             }).ToList();

            return new JsonResult { Data = listaMenu, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult insertarPlato(platos platos)
        {

            // Insertar Nuevo plato

            db.platos.Add(platos);
            db.SaveChanges();

            return new JsonResult { Data = platos.nombre_plato + " agregado correctamente", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getPlato(int id_plato)
        {

            var plato = (from p in db.platos
                         where p.id_plato == id_plato

                         select new
                         {
                             p.id_plato,
                             p.nombre_plato,
                             p.cantidad_plato
                         }).ToList();

            return new JsonResult { Data = plato, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public JsonResult editarPlato(platos plato)
        {

            platos pl = db.platos.Where(p => p.id_plato == plato.id_plato).First();
           

            string mensaje = plato.nombre_plato + " guardado correctamente";

            if (pl != null)
            {
                pl.nombre_plato = plato.nombre_plato;
                pl.cantidad_plato = plato.cantidad_plato;
                db.SaveChanges();
            }
            else
            {
                mensaje = plato.nombre_plato + " no guardado";
            }

            return new JsonResult { Data = mensaje, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult eliminarPlato(int id_plato, string nombre_plato)
        {

            platos plato = db.platos.First(p => p.id_plato == id_plato);
            string mensaje = nombre_plato + " eliminado";

            if (plato != null)
            {
                db.platos.Remove(plato);
                db.SaveChanges();
            }
            else
            {
                mensaje = nombre_plato +" no eliminado";
            }


            return new JsonResult { Data = mensaje, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

    }
}
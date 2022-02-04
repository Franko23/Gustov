using Gustov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gustov.Controllers
{
    public class MenuController : Controller
    {

        private gustovEntities db = new gustovEntities();

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        //Método para Insertar un nuevo registro
        public JsonResult insertarPlato(platos platos)
        {

            db.platos.Add(platos);
            db.SaveChanges();

            return new JsonResult { Data = platos.nombre_plato + " agregado correctamente", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Método para recuperar el registro del plato selecciondo por medio del id
        public JsonResult getPlato(int id_plato)
        {

            var plato = (from p in db.platos
                         where p.id_plato == id_plato

                         select new
                         {
                             p.id_plato,
                             p.nombre_plato,
                             p.precio_unitario,
                             p.cantidad_plato
                         }).ToList();

            return new JsonResult { Data = plato, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        //Método para enviar el registro editado
        public JsonResult editarPlato(platos plato)
        {

            platos pl = db.platos.Where(p => p.id_plato == plato.id_plato).First();


            string mensaje = plato.nombre_plato + " guardado correctamente";

            if (pl != null)
            {
                pl.nombre_plato = plato.nombre_plato;
                pl.cantidad_plato = plato.cantidad_plato;
                pl.precio_unitario = plato.precio_unitario;
                db.SaveChanges();
            }
            else
            {
                mensaje = plato.nombre_plato + " no guardado";
            }

            return new JsonResult { Data = mensaje, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Método para eliminar el registro por medio del id
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
                mensaje = nombre_plato + " no eliminado";
            }


            return new JsonResult { Data = mensaje, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        //Métodos para los test unitarios
        public JsonResult test(string nombre)
        {
            string mensaje = nombre + " guardado";

            return new JsonResult { Data = mensaje, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public static string test1(string nombre)
        {
            string mensaje = nombre + " guardado";

            return mensaje;
        }

    }
}

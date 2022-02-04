using Gustov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gustov.Controllers
{
    public class VentaController : Controller
    {
        private gustovEntities db = new gustovEntities();

        // GET: Venta
        public ActionResult Index()
        {
            return View();
        }

        //Método para guardar el nuevo registro de venta
        public JsonResult insertarVenta(registro_ventas ventas)
        {

            var plato = (from p in db.platos
                         where p.id_plato == ventas.id_plato

                         select new
                         {
                             p.nombre_plato,
                             p.precio_unitario
                         }).ToList();

            

            foreach (var item in plato)
            {
                ventas.nombre_plato = item.nombre_plato;
                ventas.precio_actual = item.precio_unitario;
            }

            ventas.fecha_venta = DateTime.Now;
            
            db.registro_ventas.Add(ventas);
            db.SaveChanges();

            return new JsonResult { Data = " Venta guardada correctamente", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}

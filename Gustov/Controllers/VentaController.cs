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

        public JsonResult insertarVenta(registro_ventas ventas)
        {

            // Insertar Nueva venta

            db.registro_ventas.Add(ventas);
            db.SaveChanges();

            return new JsonResult { Data = " Venta guardada correctamente", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}

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

        public ActionResult Index()
        {
            return View();
        }

        //Método para listar el menu en la primera carga
        public JsonResult ListarMenu()
        {

            var listaMenu = (from p in db.platos 

                             select new
                             {
                                 p.id_plato,
                                 p.nombre_plato,
                                 p.precio_unitario,
                                 p.cantidad_plato
                             }).ToList();

            return new JsonResult { Data = listaMenu, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}
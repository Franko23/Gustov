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
        string connection = ConfigurationManager.ConnectionStrings["gustovEntities"].ConnectionString;

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

        public JsonResult GetMenu()
        {
            List<platos> listaPlatos = new List<platos>();
            conn = new MySql.Data.MySqlClient.MySqlConnection(connection);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 60;
            cmd.CommandText = @"SELECT id_plato, nombre_plato, cantidad_plato
                          FROM plato";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                platos p = new platos();
                p.id_plato = reader.GetInt32(0);
                p.nombre_plato = reader.GetString(1);
                p.cantidad_plato = reader.GetInt32(2);
                
                listaPlatos.Add(p);
            }
            conn.Close();
            return new JsonResult { Data = listaPlatos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}
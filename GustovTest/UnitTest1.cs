
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GustovTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ListaMenu()
        {
            var res = new Gustov.Controllers.HomeController().ListarMenu();
            var values = new[] { 
                new {Id = 1, nombre_plato = "Picante de Pollo", cantidad_plato = 10, precio_unitario = 10.00  },
                new { Id = 2, nombre_plato = "Charque", cantidad_plato = 15, precio_unitario = 10.00 },
                new { Id = 3, nombre_plato = "Pique", cantidad_plato = 20, precio_unitario = 10.00 },
                new { Id = 3, nombre_plato = "Pailita", cantidad_plato =25, precio_unitario = 10.00 },
            };
            Assert.Equals(values,res.Data.ToString());

        }

        [TestMethod]
        public void test()
        {
            string mensaje = "Pollo guardado";

            var jsonResult = new Gustov.Controllers.MenuController().test("Pollo");
            Assert.AreEqual("Pollo guardado", jsonResult.Data.ToString());
        }

        [TestMethod]
        public void prueba()
        {
            var res = new Gustov.Controllers.ReportesController();
            Assert.Equals("2022-02-02", res.DSImprimirDiario("2022-02-02").ToString());
        }
    }
}

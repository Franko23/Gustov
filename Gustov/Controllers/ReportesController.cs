using Gustov.Models;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gustov.Controllers
{
    public class ReportesController : Controller
    {

        private gustovEntities db = new gustovEntities();

        MySql.Data.MySqlClient.MySqlConnection conn;
        string connection = ConfigurationManager.ConnectionStrings["gustovConnectionString"].ConnectionString;

        LocalReport LocalReporte = new LocalReport();

        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImprimirDiario(string fc)
        {
            
            var tipo = "PDF";
            string path = "";
            ReportDataSource rd = null;
            LocalReporte.ReportPath = Path.Combine(Server.MapPath("~/Reportes"), "VentasDiarias.rdlc");

            rd = new ReportDataSource("VentasDiarias", DSImprimirDiario(fc).Tables[0]);


            if (System.IO.File.Exists(LocalReporte.ReportPath))
            {
                LocalReporte.ReportPath = LocalReporte.ReportPath;
            }
            else
            {
                return View("Index");
            }

            LocalReporte.DataSources.Add(rd);
            string reportType = tipo;
            string mineType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>" + tipo + "</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0.1965in</MarginTop>" +
                "<MarginLeft>0.1965in</MarginLeft>" +
                "<MarginRight>0.1965in</MarginRight>" +
                "<MarginBottom>0.1965in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = LocalReporte.Render(
                reportType,
                deviceInfo,
                out mineType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mineType);

        }

        public DataSet DSImprimirDiario(string fecha)
        {
            string sql = "call reporte_diario('" + fecha + "') ; ";
            conn = new MySql.Data.MySqlClient.MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandTimeout = 60;
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            conn.Open();

            DataSet ds = new DataSet();
            da.Fill(ds, "DsReportes");
            conn.Close();
            return ds;
        }

    }
}

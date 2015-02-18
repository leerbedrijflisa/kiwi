using System;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.Web.Reporting.Models;


namespace Lisa.Kiwi.Web.Reporting.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            
            ViewBag.ActionName = "Index";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string reportType)
        {
            var report = new Report
            {
                Type = reportType
            };

            Session["Report"] = report;

            ViewBag.ActionName = "Location";

            return RedirectToAction("Location");
        }

        public ActionResult Location()
        {
            return View("Location");
        }


        [HttpPost]
        public ActionResult Location(string building = null)
        {
            if (Session["Report"] == null)
            {
                return RedirectToAction("Index");
            }
            Report report = (Report)Session["Report"];
            report.Building = building;

            ViewBag.Report = report;

            return View(report.Type);

            switch (report.Type)
            {
                case "Drugs":
                    ViewBag.ActionName = "Advanced";
                    break;
                case "Overig":
                    ViewBag.ActionName = "Advanced";
                    break;
                default:
                    ViewBag.ActionName = "SenderDetails";
                    break;
            }

            ViewBag.ActionName = "SenderDetails";


            return View();
        }

        public ActionResult SenderDetails()
        {

            return View();
        }
    }
}
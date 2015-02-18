using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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

            if (report.Type == "Drugs" || report.Type == "Overig" || report.Type == "EHBO")
                return RedirectToAction("Advanced", new { guid = report.Type });

           return View("SenderDetails");
        }

        public ActionResult Advanced(string guid)
        {
            return View(guid);
        }

        public ActionResult SenderDetails()
        {

            return View();
        }
    }
}
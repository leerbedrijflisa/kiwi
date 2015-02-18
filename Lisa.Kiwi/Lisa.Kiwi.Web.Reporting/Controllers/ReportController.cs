using System;
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
        public ActionResult Location(string a = null)
        {
            var report = (Report)Session["Report"];

            ViewBag.ActionName = "SenderDetails";


            return View();
        }

        public ActionResult SenderDetails()
        {

            return View();
        }
    }
}
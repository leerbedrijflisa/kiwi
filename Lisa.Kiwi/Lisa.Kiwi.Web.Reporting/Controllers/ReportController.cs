using System.Collections.Generic;
using System.Web.Mvc;
using Lisa.Kiwi.Web.Reporting.Models;
using Lisa.Kiwi.Tools;
using System;

namespace Lisa.Kiwi.Web.Reporting.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Type()
        {
            var types = new string[]
            {
                "Drugs",
                "Overlast",
                "Voertuigen",
                "Inbraak",
                "Diefstal",
                "Intimidatie",
                "Pesten",
                "Digitaal grensoverschrijdend gedrag",
                "Etc"
            };

            ViewBag.ReportTypes = new SelectList(types);

            return View();
        }

        [HttpPost]
        public ActionResult Details(OriginalReport data, string reportType, string nextBtn)
        {
            ViewBag.ReportType = reportType;
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    string guid = Guid.NewGuid().ToString();

                    OriginalReport report = new OriginalReport();
                    report.Location = data.Location;
                    report.Time = data.Time;
                    report.Description = data.Description;
                    report.Type = reportType;
                    report.Guid = guid;
     
                    return View("ContactDetails");
                }
            }
            return View();
        }

        public ActionResult ContactDetails(string guid)
        {
            ViewBag.Guid = guid;
            return View("ContactDetails");
        }

        [HttpPost]
        public ActionResult Contactdetails(Contact contact, string guid)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Save contact details

            return View("Confirmed");
        }

        public ActionResult Confirmed()
        {
            return View();
        }

        private bool CheckReport(OriginalReport report, bool checkEmpty = true, string[] skipField = null)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            return true;
        }
        
    }
}
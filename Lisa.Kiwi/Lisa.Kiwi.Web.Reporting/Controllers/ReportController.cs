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
        public ActionResult Details(string reportType)
        {
            if (!ModelState.IsValid)
            {
                return View("Type");
            }
            ViewBag.Type = reportType;
            return View();
        }

        [HttpPost]
        public ActionResult Check(OriginalReport report, string contactMe = "false")
        {
            if (!CheckReport(report))
            {
                // Hier opslaan!
                return View("Report");
            }

            if (contactMe != "false")
            {
                return ContactDetails(report.Guid);
            }
            return RedirectToAction("Confirmed");
        }

        public ActionResult ContactDetails(string guid)
        {
            ViewBag.Guid = guid;
            return View("ContactDetails");
        }

        [HttpPost]
        public ActionResult Contactdetails(Contact contact, string guid)
        {
            if (string.IsNullOrEmpty(contact.Email) && contact.PhoneNumber == 0 && contact.StudentNumber == 0)
            {
                ModelState.AddModelError("Form", "U moet een van de contact velden invoeren (Telefoon, Email of studenten nummer).");
            } 

            if (contact.Email == "false")
            {
                contact.Email = "";
            }



            if (!ModelState.IsValid)
            {
                return View("Contactdetails");
            }

            // Save contact details

            return RedirectToAction("Confirmed");
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
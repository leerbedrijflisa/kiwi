using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.Web.Reporting.Models;
using Lisa.Kiwi.Tools;

namespace Lisa.Kiwi.Web.Reporting.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportType()
        {
            List<string> types = new List<string>()
            {
                "Drugs", "Overlast", "Voertuigen", "Inbraak", "Diefstal", "Intimidatie", "Pesten", "Digitaal grensoverschrijdend gedrag", "Etc"
            };

            ViewBag.ReportTypes = types;
            return View();
        }

        [HttpPost]
        public ActionResult Report(OriginalReport report)
        {
            var rand = new Random().Next(0000, 9999);
            var id = "M" + DateTime.Now.ToString("yyyyMMdd") + "-" + DateTime.Now.ToString("HHmmss") + "-" + rand;
            report.GuId = id;

            return View();
        }

        [HttpPost]
        public ActionResult ReportDetails(OriginalReport report, string contactMe = "false")
        {
            if (!CheckReport(report))
            {
                // Hier opslaan!
                return View("Report");
            }

            if (contactMe != "false")
            {
                return ContactDetails(report.GuId);
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
            if (string.IsNullOrEmpty(contact.Email) && contact.Phone == 0 && contact.StudentNo == 0)
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
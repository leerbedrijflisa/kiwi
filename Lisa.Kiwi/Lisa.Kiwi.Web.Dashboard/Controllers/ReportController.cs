using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi.Access;
using System.Threading.Tasks;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
    public class ReportController : Controller
    {

        private TempReports Reports = new TempReports();

        public ActionResult Index(string id)
        {
            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var report = Reports.GetReport(id);
            var statusses = Enum.GetValues(typeof(StatusName)).Cast<StatusName>().ToList();
            ViewBag.Statusses = statusses;

            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string id, string guid, string Status, string Comment)
        {
            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var report = Reports.GetReport(id);
            var statusses = Enum.GetValues(typeof(StatusName)).Cast<StatusName>().ToList();
            ViewBag.Statusses = statusses;

            if (id != guid)
            {
                ModelState.AddModelError("guid", "Geen geldig id.");
            }
            if (!Enum.IsDefined(typeof(StatusName), Status))
            {
                ModelState.AddModelError("Status", "Geen geldige status.");
            }

            if (!ModelState.IsValid)
            {
                return View(report);
            }

            if (report.Status.Last().Name.ToString() != Status)
            {
                Reports.SaveStatus(guid, Status);
            }

            if (!string.IsNullOrEmpty(Comment))
            {
                Reports.SaveComment(guid, Comment);
            }

            return View(report);
        }
    }
}
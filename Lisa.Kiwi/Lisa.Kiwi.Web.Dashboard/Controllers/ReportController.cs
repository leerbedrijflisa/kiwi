using System;
using System.Linq;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi.Access;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index(int id)
        {
            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var reports = ReportProxy.GetReports();

            var report = reports.Where(r => r.Id == id).ToList()[0];

            var statusses = Enum.GetValues(typeof(StatusName)).Cast<StatusName>().ToList();

            ViewBag.Statusses = statusses;

            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int id, string guid, string status, string comment)
        {
            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var reports = ReportProxy.GetReports();

            var report = reports.Where(r => r.Id == id);

            var statusses = Enum.GetValues(typeof(StatusName)).Cast<StatusName>().ToList();
            ViewBag.Statusses = statusses;

            return View(report);
        }
    }
}
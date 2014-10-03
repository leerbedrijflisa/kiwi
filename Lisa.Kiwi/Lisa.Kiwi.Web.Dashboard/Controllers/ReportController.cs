using System;
using System.Linq;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi.Access;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var reports = ReportProxy.GetReports();

            var reportsData = reports.Where(r => r.Status != StatusName.Solved);

            return View(reportsData);
        }

        public ActionResult Details(int id)
        {
            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var reports = ReportProxy.GetReports();

            var report = reports.Where(r => r.Id == id).FirstOrDefault();

            var statusses = Enum.GetValues(typeof(StatusName)).Cast<StatusName>().ToList();

            ViewBag.Statusses = statusses;

            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id, string status, string comment)
        {
            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }


            return RedirectToAction("Report", "Details", id);
        }
    }
}
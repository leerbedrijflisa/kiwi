using System;
using System.Linq;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;
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

            var report = ReportProxy.GetReports().Where(r => r.Id == id).FirstOrDefault();

            var statuses = Enum.GetValues(typeof(StatusName)).Cast<StatusName>().ToList();

            ViewBag.Statuses = statuses;

            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id, StatusName? status, string comment = null)
        {
            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var report = ReportProxy.GetReports().Where(r => r.Id == id).FirstOrDefault();

            if (report == null)
            {
                return HttpNotFound();
            }

            if (status != null)
            {
                var newStatus = new Status
                {
                    Created = DateTimeOffset.Now,
                    Name = (StatusName) status,
                    Report = report.Id
                };

                StatusProxy.AddStatus(newStatus);
            }

            return RedirectToAction("Details", new { id = id });
        }
    }
}
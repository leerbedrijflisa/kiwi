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
            var statusses = new SelectList(Enum.GetValues(typeof(StatusName)).Cast<StatusName>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");
            ViewBag.Statusses = statusses;

            return View(report);
        }
    }
}
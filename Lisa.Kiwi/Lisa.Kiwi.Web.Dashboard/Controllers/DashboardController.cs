using System.Linq;
using System.Web.Mvc;
using Lisa.Kiwi.Data;
using Lisa.Kiwi.WebApi.Access;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
    public class DashboardController : Controller
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
    }
}
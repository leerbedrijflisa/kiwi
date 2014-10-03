using System.Web.Mvc;

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

            var reportsData = WebApi.Access.Proxies.Report.GetAllReports();
            return View(reportsData);
        }
    }
}
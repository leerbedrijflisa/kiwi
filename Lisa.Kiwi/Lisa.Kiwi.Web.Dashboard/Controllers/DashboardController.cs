using System.Web.Mvc;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            //var reportsData = WebApi.Access.Proxies.Report.GetAllReportsByStatusIsNot("Solved");
            var reportsData = WebApi.Access.Proxies.Report.GetAllReports();

            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            
            //reportsData = reportsData
            //    .Where(r => r.Status.Last().Name != StatusName.Solved)
            //    .OrderBy(r => r.Created);

            return View(reportsData);
        }
    }
}
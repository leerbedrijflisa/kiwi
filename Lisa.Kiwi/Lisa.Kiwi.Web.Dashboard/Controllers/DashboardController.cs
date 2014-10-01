using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi.Access;
using System.Threading.Tasks;
using Lisa.Kiwi.Web.Dashboard.Models;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var reportsData = new TempReports().GetAll();
            reportsData = reportsData
                .Where(r => r.Status.Name != StatusName.Solved)
                .OrderBy(r => r.Created);

            return View(reportsData);          
        }

        public ActionResult Report(string id)
        {
            return View();
        }
    }
}
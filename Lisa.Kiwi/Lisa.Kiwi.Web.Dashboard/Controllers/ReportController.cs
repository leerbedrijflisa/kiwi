using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            var sessionTimeOut = Session.Timeout = 60;

            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View("report");
            }
        }
    }
}
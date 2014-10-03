﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi.Access;
using System.Threading.Tasks;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        private TempReports Reports = new TempReports();

        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var reportsData = Reports.GetAll();
            reportsData = reportsData
                .Where(r => r.Status.Last().Name != StatusName.Solved)
                .OrderBy(r => r.Created);

            return View(reportsData);
        }
    }
}
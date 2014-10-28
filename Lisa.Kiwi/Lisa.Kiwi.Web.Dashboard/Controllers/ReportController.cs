using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;
using Lisa.Kiwi.Data;
using Lisa.Kiwi.Web.Dashboard.Models;
using Lisa.Kiwi.Web.Dashboard.Utils;

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
            var reportSettings = ReportSettingsProxy.GetReportSettings();

            var reportsData = reports;
            

            if (Session["user"].ToString() != "beveiliger")
            {
                reportsData = reports.Where(r => r.Status != StatusName.Solved );
                foreach (var item in reportSettings)
                {
                    if (item.Visible != false)
                    {

                    }
                }
            }



            return View(reportsData);
        }

        //var provider = new AssociatedMetadataTypeTypeDescriptionProvider(typeof(ReportProxy), typeof(ReportMetadata));
        //    TypeDescriptor.AddProviderTransparent(provider, typeof(ReportProxy));
        // code for the metadata from a other model

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
            ViewBag.Visible = ReportSettingsProxy.GetReportSettings().Where(rs => rs.Report == report.Id).FirstOrDefault().Visible;

            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id, StatusName? status, string remark, bool Visibility = false)
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

            if (remark != null)
            {
                var newRemark = new Remark
                {
                    Description = remark,
                    Created = DateTimeOffset.Now,
                    Report = report.Id
                };

                RemarkProxy.AddRemark(newRemark);
            }

            if (Session["user"].ToString() == "beveiliger")
            {
                var settingsId = ReportSettingsProxy.GetReportSettings().Where(r => r.Report == report.Id).FirstOrDefault().Id;
                var reportSettings = new ReportSettings
                {
                    Id = settingsId,
                    Visible = Visibility,
                    Report = report.Id
                };

                ReportSettingsProxy.AddSettings(reportSettings);
            }

            return RedirectToAction("Details", new { id = id });
        }
    }
}
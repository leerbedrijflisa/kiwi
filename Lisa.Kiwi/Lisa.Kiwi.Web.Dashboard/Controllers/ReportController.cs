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
        public ActionResult Details(int id, StatusName? status, string remark, bool Visibility)
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
                ReportSettingsProxy.UpdateVisibility(report.Id, Visibility);
            }

            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult Fill()
        {
            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            ContactProxy.AddContact(new Contact
            {
                EmailAddress = "beyonce@gijs.nl",
                Name = "Gijs Jannssenn",
                PhoneNumber = "0655889944",
                StudentNumber = 99015221
            });

            var contact = ContactProxy.GetContacts().Where(c => c.Name == "Gijs Jannssenn").FirstOrDefault();

            contact = new Contact
            {
                Id = contact.Id,
                EmailAddress = "beyonce@gijs.nl",
                Name = "Gijs Jannssenn",
                PhoneNumber = "0655889944",
                StudentNumber = 99015221
            };

            ReportProxy.AddReport(new Report
            {
                Created = DateTime.UtcNow,
                Status = StatusName.Open,
                Description = "Er word drugs gedealed",
                Ip = "180.16.52.39",
                Location = "Arco Baleno",
                Time = DateTime.Today.AddHours(8),
                Type = ReportType.Drugs,
                UserAgent = "Opera",
                Guid = Guid.NewGuid().ToString()//,
                //Contacts = contact
            });

            ReportProxy.AddReport(new Report
            {
                Created = DateTime.UtcNow,
                Status = StatusName.InProgress,
                Description = "Wiet verkoop",
                Ip = "177.75.22.11",
                Location = "Sportcentrum",
                Time = DateTime.Today.AddHours(3).AddMinutes(34),
                Type = ReportType.Drugs,
                UserAgent = "Chrome",
                Guid = Guid.NewGuid().ToString()
            });

            ReportProxy.AddReport(new Report
            {
                Created = DateTime.UtcNow,
                Status = StatusName.Open,
                Description = "Rook gesignaleerd",
                Ip = "10.180.14.6",
                Location = "Ocra",
                Time = DateTime.Today.AddHours(7).AddMinutes(21),
                Type = ReportType.Fire,
                UserAgent = "Chrome",
                Guid = Guid.NewGuid().ToString()
            });

            ReportProxy.AddReport(new Report
            {
                Created = DateTime.UtcNow,
                Status = StatusName.Open,
                Description = "Gebroken raam, verdwenen computer",
                Ip = "10.180.14.47",
                Location = "Bianco begane grond",
                Time = DateTime.Today.AddHours(11).AddMinutes(30),
                Type = ReportType.Theft,
                UserAgent = "Chrome",
                Guid = Guid.NewGuid().ToString()
            });

            return RedirectToAction("Index");
        }

        private ReportProxy ReportProxy = new ReportProxy();
        private StatusProxy StatusProxy = new StatusProxy();
        private RemarkProxy RemarkProxy = new RemarkProxy();
        private ContactProxy ContactProxy = new ContactProxy();
        private ReportSettingsProxy ReportSettingsProxy = new ReportSettingsProxy();
    }
}
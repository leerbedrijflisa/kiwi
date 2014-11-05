using System;
using System.Linq;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;
using Lisa.Kiwi.Data;
using System.Collections.Generic;
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
            List<Report> reportsData = new List<Report>();

            if (Session["user"].ToString() == "user")
            {
                reports = reports.Where(r => r.Status != StatusName.Solved);
                foreach (var report in reports)
                {
                    if (report.Hidden == false)
                    {
                        reportsData.Add(report);
                    }
                }
            }
            else
            {
                reportsData.AddRange(reports);
            }
            return View(reportsData.OrderByDescending(r => r.Created));
        }

        public ActionResult Details(int id)
        {
            var sessionTimeOut = Session.Timeout = 60;
            if (Session["user"] == null || sessionTimeOut == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var report = ReportProxy.GetReports(true).Where(r => r.Id == id).FirstOrDefault();

            if (Session["user"].ToString() == "user")
            {
                if (report.Status == StatusName.Solved || (report.Hidden == true))
                {
                    return RedirectToAction("Index", "Report");
                }
            }

            var statuses = Enum.GetValues(typeof(StatusName)).Cast<StatusName>().ToList();
            ViewBag.Statuses = statuses;
            ViewBag.Visible = report.Hidden;

            var remarks = RemarkProxy.GetRemarks()
                .Where(r => r.Report == report.Id)
                .OrderByDescending(r => r.Created);

            var statusses = StatusProxy.GetStatuses()
                .Where(r => r.Report == report.Id);

            List<Models.Remark> LogbookData = new List<Models.Remark>();
            LogbookData.AddRange(AddRemarksToLogbook(remarks));
            LogbookData.AddRange(AddStatussesToLogbook(statusses, report));

            ViewBag.Remarks = LogbookData.OrderByDescending(r => r.Created);

            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id, StatusName? status, string remark, bool Visibility = true)
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

            if (remark != null && !string.IsNullOrEmpty(remark))
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
                report.Hidden = Visibility;
                ReportProxy.AddReport(report);
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

            var contact = ContactProxy.AddContact(new Contact
            {
                EmailAddress = "beyonce@gijs.nl",
                Name = "Gijs Jannssenn",
                PhoneNumber = "0655889944",
                StudentNumber = 99015221
            });

            var report = new Report
            {
                Created = DateTime.UtcNow,
                Status = StatusName.Open,
                Description = "Ik word gepest",
                Ip = "244.255.63.39",
                Location = "Arco Baleno",
                Time = DateTime.Today.AddHours(3),
                Hidden = false,
                Type = ReportType.Bullying,
                UserAgent = "Opera",
                Guid = Guid.NewGuid().ToString()
                
            }; 
            
            report.Contacts.Add(contact);
            ReportProxy.AddReport(report);


            report = new Report
            {
                Created = DateTime.UtcNow,
                Status = StatusName.Open,
                Description = "Gijs pest mij",
                Ip = "190.11.22.86",
                Location = "Azurro",
                Time = DateTime.Today.AddHours(4),
                Hidden = false,
                Type = ReportType.Bullying,
                UserAgent = "Opera",
                Guid = Guid.NewGuid().ToString()
            };

            report.Contacts.Add(contact);
            ReportProxy.AddReport(report);

            report = new Report
            {
                Created = DateTime.UtcNow,
                Status = StatusName.Open,
                Description = "Ik weer word gepest!",
                Ip = "244.255.63.39",
                Location = "Arco Baleno",
                Time = DateTime.Today.AddHours(7),
                Hidden = false,
                Type = ReportType.Bullying,
                UserAgent = "Opera",
                Guid = Guid.NewGuid().ToString()
            };

            report.Contacts.Add(contact);
            ReportProxy.AddReport(report);

            ReportProxy.AddReport(new Report
            {
                Created = DateTime.UtcNow,
                Status = StatusName.Open,
                Description = "Er word drugs gedealed",
                Ip = "180.16.52.39",
                Location = "Arco Baleno",
                Time = DateTime.Today.AddHours(8),
                Hidden = false,
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
                Hidden = false,
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
                Hidden = false,
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
                Hidden = false,
                Type = ReportType.Theft,
                UserAgent = "Chrome",
                Guid = Guid.NewGuid().ToString()
            });

            return RedirectToAction("Index");
        }

        private List<Models.Remark> AddRemarksToLogbook(IQueryable<Remark> remarks)
        {
            List<Models.Remark> result = new List<Models.Remark>();
            foreach (var remark in remarks)
            {
                result.Add(new Models.Remark
                {
                    Created = remark.Created,
                    Description = remark.Description,
                    User = Session["user"].ToString()
                });
            }
            return result;
        }

        private List<Models.Remark> AddStatussesToLogbook(IQueryable<Status> statusses, Report report)
        {
            List<Models.Remark> result = new List<Models.Remark>();

            var lastStatus = string.Empty;
            foreach (var status in statusses)
            {
                var description = CreateLogbookStatusDescription(status, lastStatus, report);
                lastStatus = status.Name.ToString();

                result.Add(new Models.Remark
                {
                    Created = status.Created,
                    Description = description,
                    User = Session["user"].ToString()
                });
            }
            return result;
        }

        private string CreateLogbookStatusDescription(Status status, string lastStatus, Report report)
        {
            var description = "";
            if (string.IsNullOrEmpty(lastStatus))
            {
                var person = "Anoniem";
                if (report.Contacts.Count > 0)
                {
                    person = report.Contacts[0].Name;
                }
                description = string.Format("Melding is aangemaakt door: {0} met de status {1}.", person, status.Name.GetStatusDisplayNameFromMetadata());
            }
            else
            {
                description = string.Format("The Status {0} is changed to {1}.", lastStatus, status.Name.GetStatusDisplayNameFromMetadata());
            }
            return description;
        }

        private ReportProxy ReportProxy = new ReportProxy();
        private StatusProxy StatusProxy = new StatusProxy();
        private RemarkProxy RemarkProxy = new RemarkProxy();
        private ContactProxy ContactProxy = new ContactProxy();
    }
}
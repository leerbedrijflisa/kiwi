using System;
using System.Linq;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;
using Lisa.Kiwi.Data;
using System.Collections.Generic;

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

                    if (report.Status == null)
                    {
                        report.Status = StatusName.Solved;
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

            var report = ReportProxy.GetReports().Where(r => r.Id == id).FirstOrDefault();

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

            var remarks = RemarkProxy.GetRemarks().Where(r => r.Report == report.Id).OrderByDescending(r => r.Created);
            List<Remark> remarksData = new List<Remark>();
            foreach (var remark in remarks)
            {
                remarksData.Add(remark);
            }

            ViewBag.Remarks = remarksData;

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

                var statusRemark = new Remark
                {
                    Description = string.Format("De status is veranderd van {0} naar {1}", report.Status.ToString(), status.ToString()),
                    Created = DateTime.Now,
                    Report = report.Id
                };
                RemarkProxy.AddRemark(statusRemark);
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

        private ReportProxy ReportProxy = new ReportProxy();
        private StatusProxy StatusProxy = new StatusProxy();
        private RemarkProxy RemarkProxy = new RemarkProxy();
        private ContactProxy ContactProxy = new ContactProxy();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Lisa.Kiwi.Data;
using Lisa.Kiwi.Web.Dashboard.Utils;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;
using LogBookEntry = Lisa.Kiwi.Web.Dashboard.Models.LogBookEntry;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
	public class ReportController : Controller
	{
		private const string DefaultSortBy = "Created DESC";

        public ReportController()
        {
            _token = System.Web.HttpContext.Current.Session["token"] as string ?? "";
            _tokenType = System.Web.HttpContext.Current.Session["token_type"] as string ?? "";

            _statusProxy = new StatusProxy(ConfigHelper.GetODataUri(), _token, _tokenType);
            _reportProxy = new ReportProxy(ConfigHelper.GetODataUri(), _token, _tokenType);
            _remarkProxy = new RemarkProxy(ConfigHelper.GetODataUri(), _token, _tokenType);
            _contactProxy = new ContactProxy(ConfigHelper.GetODataUri(), _token, _tokenType);
        }

		public ActionResult Index(string sortBy = DefaultSortBy)
		{
			var sessionTimeOut = Session.Timeout = 60;
			if (Session["user"] == null || sessionTimeOut == 0)
			{
				return RedirectToAction("Login", "Account");
			}

			IQueryable<Report> reports;

			if (Session["user"].ToString() == "beveiliger")
			{
				reports = _reportProxy.GetReports()
					.Where(r => r.Status != StatusName.Solved)
					.Where(report => report.Hidden == false);
			}
			else
			{
				reports = _reportProxy.GetReports();
			}

			try
			{
				ViewBag.SortingBy = sortBy;
				return View(reports.SortBy(sortBy).ToList());
			}
			catch (ArgumentException)
			{
				// sortBy was invalid, use the default
				ViewBag.SortingBy = DefaultSortBy;
				return View(reports.SortBy(DefaultSortBy));
			}
		}

		public ActionResult Details(int id)
		{
			var sessionTimeOut = Session.Timeout = 60;
			if (Session["user"] == null || sessionTimeOut == 0)
			{
				return RedirectToAction("Login", "Account");
			}

            var report = _reportProxy.GetReports().Where(r => r.Id == id).FirstOrDefault();

			if (Session["user"].ToString() == "user")
			{
				if (report.Status == StatusName.Solved || report.Hidden)
				{
					return RedirectToAction("Index", "Report");
				}
			}

			var statuses = Enum.GetValues(typeof (StatusName)).Cast<StatusName>().ToList();
			ViewBag.Statuses = statuses;
			ViewBag.Visible = report.Hidden;

			var remarks = _remarkProxy.GetRemarks()
				.Where(r => r.Report == report.Id)
				.OrderByDescending(r => r.Created);

			var statusses = _statusProxy.GetStatuses()
				.Where(r => r.Report == report.Id);

            List<LogBookEntry> LogbookData = new List<LogBookEntry>();
			LogbookData.AddRange(AddRemarksToLogbook(remarks));
			LogbookData.AddRange(AddStatussesToLogbook(statusses, report));

			ViewBag.Remarks = LogbookData.OrderByDescending(r => r.Created);

			return View(report);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        [ValidateInput(false)]
		public ActionResult Details(int id, StatusName? status, string remark, bool visibility = true)
		{
			var sessionTimeOut = Session.Timeout = 60;
			if (Session["user"] == null || sessionTimeOut == 0)
			{
				return RedirectToAction("Login", "Account");
			}

            // System.NotSupportedException: The method 'FirstOrDefault' is not supported. - When using single call
			var report = _reportProxy.GetReports().Where(r => r.Id == id).FirstOrDefault();

			if (report == null)
			{
				return HttpNotFound();
			}

			if (status != null)
			{
			    if (status != report.Status)
			    {
			        var newStatus = new Status
			        {
			            Created = DateTimeOffset.UtcNow,
			            Name = (StatusName) status,
			            Report = report.Id
			        };

                    _statusProxy.AddStatus(newStatus);
			    }
			}

			if (remark != null && !string.IsNullOrEmpty(remark))
			{
				var newRemark = new WebApi.Remark
				{
					Description = remark,
					Created = DateTimeOffset.UtcNow,
					Report = report.Id
				};

				_remarkProxy.AddRemark(newRemark);
			}
            
			if ((bool)Session["is_admin"])
			{
				report.Hidden = visibility;                

			    _reportProxy.SaveReport(report);
			}

			return RedirectToAction("Details", new {id});
		}

		public ActionResult Fill()
		{
			var sessionTimeOut = Session.Timeout = 60;
			if (Session["user"] == null || sessionTimeOut == 0)
			{
				return RedirectToAction("Login", "Account");
			}

            var report = _reportProxy.AddReport(new WebApi.Report
			{
				Created = DateTime.UtcNow,
				Status = StatusName.Open,
				Description = "Ik word gepest - with user",
				Ip = "244.255.63.39",
				Location = "Arco Baleno - with user",
				Time = DateTime.Today.AddHours(3),
				Hidden = false,
				Type = "Pesten",
				UserAgent = "Opera",
				Guid = Guid.NewGuid().ToString()
			});

            var reportId = _reportProxy.GetReports().Where(r => r.Guid == report.Guid).FirstOrDefault().Id;
            _contactProxy.AddContact(new Contact
            {
                EmailAddress = "beyonce@gijs.nl",
                Name = "Gijs Jannssenn",
                PhoneNumber = "0655889944",
                StudentNumber = 99015221,
                Report = reportId
            });

            report = _reportProxy.AddReport(new WebApi.Report
			{
				Created = DateTime.UtcNow,
				Status = StatusName.Open,
				Description = "Gijs pest mij",
				Ip = "190.11.22.86",
				Location = "Azurro",
				Time = DateTime.Today.AddHours(4),
				Hidden = false,
				Type = "Pesten",
				UserAgent = "Opera",
                Guid = Guid.NewGuid().ToString()
			});

            report = _reportProxy.AddReport(new WebApi.Report
			{
				Created = DateTime.UtcNow,
				Status = StatusName.Open,
				Description = "Ik weer word gepest!",
				Ip = "244.255.63.39",
				Location = "Arco Baleno",
				Time = DateTime.Today.AddHours(7),
				Hidden = false,
				Type = "Pesten",
				UserAgent = "Opera",
				Guid = Guid.NewGuid().ToString()
			});

            report = _reportProxy.AddReport(new WebApi.Report
			{
				Created = DateTime.UtcNow,
				Status = StatusName.Open,
				Description = "Er word drugs gedealed",
				Ip = "180.16.52.39",
				Location = "Arco Baleno",
				Time = DateTime.Today.AddHours(8),
				Hidden = false,
				Type = "Drugs",
				UserAgent = "Opera",
				Guid = Guid.NewGuid().ToString() //,
				//Contacts = contact
			});

            report = _reportProxy.AddReport(new WebApi.Report
			{
				Created = DateTime.UtcNow,
				Status = StatusName.InProgress,
				Description = "Wiet verkoop",
				Ip = "177.75.22.11",
				Location = "Sportcentrum",
				Time = DateTime.Today.AddHours(3).AddMinutes(34),
				Hidden = false,
				Type = "Drugs",
				UserAgent = "Chrome",
				Guid = Guid.NewGuid().ToString()
			});

            report = _reportProxy.AddReport(new WebApi.Report
			{
				Created = DateTime.UtcNow,
				Status = StatusName.Open,
				Description = "Rook gesignaleerd",
				Ip = "10.180.14.6",
				Location = "Ocra",
				Time = DateTime.Today.AddHours(7).AddMinutes(21),
				Hidden = false,
				Type = "Brand",
				UserAgent = "Chrome",
				Guid = Guid.NewGuid().ToString()
			});

            report = _reportProxy.AddReport(new WebApi.Report
			{
				Created = DateTime.UtcNow,
				Status = StatusName.Open,
				Description = "Gebroken raam, verdwenen computer",
				Ip = "10.180.14.47",
				Location = "Bianco begane grond",
				Time = DateTime.Today.AddHours(11).AddMinutes(30),
				Hidden = false,
				Type = "Diefstal",
				UserAgent = "Chrome",
				Guid = Guid.NewGuid().ToString()
			});

			return RedirectToAction("Index");
		}

        private List<LogBookEntry> AddRemarksToLogbook(IQueryable<WebApi.Remark> remarks)
		{
            List<LogBookEntry> result = new List<LogBookEntry>();
			foreach (var remark in remarks)
			{
                result.Add(new LogBookEntry
				{
					Created = remark.Created,
					Description = remark.Description,
					User = Session["user"].ToString()
				});
			}
			return result;
		}

        private List<LogBookEntry> AddStatussesToLogbook(IQueryable<Status> statusses, WebApi.Report report)
		{
            List<LogBookEntry> result = new List<LogBookEntry>();
            Status lastStatus = null;

			foreach (var status in statusses)
			{
                var description = CreateLogbookStatusDescription(status, report.Id, lastStatus);
				lastStatus = status;

                result.Add(new LogBookEntry
				{
					Created = status.Created,
					Description = description,
					User = Session["user"].ToString()
				});
			}
			return result;
		}

        private string CreateLogbookStatusDescription(Status status, int reportId, Status lastStatus = null)
		{
			var description = "";
			if (lastStatus == null)
			{
				var person = "Anoniem";
                var contact = _contactProxy.GetContacts().Where(c => c.Report == reportId).FirstOrDefault();
                if (contact != null)
                {
                    person = contact.Name;
                }
                // TODO: contacts
                //if (report.Contacts.Count > 0)
                //{
                //    person = report.Contacts[0].Name;
                //}
				description = string.Format("Melding is aangemaakt door: {0} met de status {1}.", person,
					status.Name.GetStatusDisplayNameFromMetadata());
			}
			else
			{
				description = string.Format("The Status {0} is changed to {1}.", lastStatus.Name.GetStatusDisplayNameFromMetadata(),
					status.Name.GetStatusDisplayNameFromMetadata());
			}
			return description;
		}

		private readonly ContactProxy _contactProxy;
        private readonly RemarkProxy _remarkProxy;
        private readonly ReportProxy _reportProxy;
        private readonly StatusProxy _statusProxy;
	    private string _token = "";
	    private string _tokenType = "";
	}
}
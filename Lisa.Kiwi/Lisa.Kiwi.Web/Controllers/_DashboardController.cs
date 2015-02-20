//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using System.Web.UI.WebControls;
//using Lisa.Kiwi.Data;
//using Lisa.Kiwi.Web.Dashboard.Models;
//using Lisa.Kiwi.Web.Dashboard.Utils;
//using Lisa.Kiwi.WebApi;
//using Lisa.Kiwi.WebApi.Access;
//using Resources;
//using LogBookEntry = Lisa.Kiwi.Web.Dashboard.Models.LogBookEntry;

//namespace Lisa.Kiwi.Web.Dashboard.Controllers
//{
//    public class DashboardController : Controller
//    {
//        public DashboardController()
//        {
//            var token = System.Web.HttpContext.Current.Session["token"] as string ?? "";
//            var tokenType = System.Web.HttpContext.Current.Session["token_type"] as string ?? "";

//            _statusProxy = new StatusProxy(ConfigHelper.GetODataUri(), token, tokenType);
//            _reportProxy = new ReportProxy(ConfigHelper.GetODataUri(), token, tokenType);
//            _remarkProxy = new RemarkProxy(ConfigHelper.GetODataUri(), token, tokenType);
//            _contactProxy = new ContactProxy(ConfigHelper.GetODataUri(), token, tokenType);
//        }

//        public ActionResult Index(int id = 1, string sortBy = DefaultSortBy)
//        {
//            var sessionTimeOut = Session.Timeout = 60;
//            if (Session["user"] == null || sessionTimeOut == 0)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            int page = 0;
//            if (id > 0)
//            {
//                page = id - 1;
//            }
//            int items = DefaultItems;

//            IQueryable<Report> reports;

//            if (!(bool)Session["is_admin"])
//            {
//                reports = _reportProxy.GetReports()
//                    .Where(r => r.Status != StatusName.Solved)
//                    .Where(report => report.Hidden == false);
//            }
//            else
//            {
//                reports = _reportProxy.GetReports();
//            }

//            ViewBag.pages = (int)Math.Ceiling((double)reports.Count() / (double)items);
//            ViewBag.currentPage = page;
//            ViewBag.SignalRUrl = ConfigHelper.GetSignalRUri();

//            try
//            {
//                ViewBag.SortingBy = sortBy;
//                return View(reports.SortBy(sortBy).ToList().Skip(items * page).Take(items));
//            }
//            catch (ArgumentException)
//            {
//                // sortBy was invalid, use the default
//                ViewBag.SortingBy = DefaultSortBy;
//                return View(reports.SortBy(DefaultSortBy).Skip(items * page).Take(items));
//            }
//        }

//        public ActionResult Details(int? id)
//        {
//            var sessionTimeOut = Session.Timeout = 60;
//            if (Session["user"] == null || sessionTimeOut == 0)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            if (id == null)
//            {
//                return View("Error404");
//            }

//            var report = _reportProxy.GetReports().Where(r => r.Id == id).FirstOrDefault();

//            if (Session["user"].ToString() == "user")
//            {
//                if (report.Status == StatusName.Solved || report.Hidden)
//                {
//                    return RedirectToAction("Index", "Report");
//                }
//            }
//            if (!(bool)Session["is_admin"] && report.Hidden || report.Status == StatusName.Solved)
//            {
//                return RedirectToAction("Index", "Report");
//            }

//            var contact = _contactProxy.GetContacts().Where(c => c.Report == report.Id).FirstOrDefault();
//            ViewBag.contact = contact;

//            var statuses = Enum.GetValues(typeof(StatusName)).Cast<StatusName>().ToList();
//            ViewBag.Statuses = statuses;
//            ViewBag.Visible = report.Hidden;

//            var remarks = _remarkProxy.GetRemarks()
//                .Where(r => r.Report == report.Id)
//                .OrderByDescending(r => r.Created);

//            var statusses = _statusProxy.GetStatuses()
//                .Where(r => r.Report == report.Id);

//            List<LogBookEntry> LogbookData = new List<LogBookEntry>();
//            LogbookData.AddRange(AddRemarksToLogbook(remarks));
//            LogbookData.AddRange(AddStatussesToLogbook(statusses, report));

//            ViewBag.Remarks = LogbookData.OrderByDescending(r => r.Created);

//            return View(report);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [ValidateInput(false)]
//        public ActionResult Details(int id, StatusName? status, string remark, bool visibility = true)
//        {
//            var sessionTimeOut = Session.Timeout = 60;
//            if (Session["user"] == null || sessionTimeOut == 0)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            // System.NotSupportedException: The method 'FirstOrDefault' is not supported. - When using single call
//            var report = _reportProxy.GetReports().Where(r => r.Id == id).FirstOrDefault();

//            if (report == null)
//            {
//                return HttpNotFound();
//            }

//            if (status != null)
//            {
//                if (status != report.Status)
//                {
//                    var newStatus = new Status
//                    {
//                        Created = DateTimeOffset.UtcNow,
//                        Name = (StatusName)status,
//                        Report = report.Id
//                    };

//                    _statusProxy.AddStatus(newStatus);
//                }
//            }

//            if (remark != null && !string.IsNullOrEmpty(remark))
//            {
//                var newRemark = new WebApi.Remark
//                {
//                    Description = remark,
//                    Created = DateTimeOffset.UtcNow,
//                    Report = report.Id
//                };

//                _remarkProxy.AddRemark(newRemark);
//            }

//            if ((bool)Session["is_admin"])
//            {
//                report.Hidden = visibility;

//                _reportProxy.SaveReport(report);
//            }

//            return RedirectToAction("Details", new { id });
//        }

//        public ActionResult Fill()
//        {
//            var sessionTimeOut = Session.Timeout = 60;
//            if (Session["user"] == null || sessionTimeOut == 0)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            var report = _reportProxy.AddReport(new WebApi.Report
//            {
//                Created = DateTime.UtcNow.AddHours(3),
//                Status = StatusName.Open,
//                Description = "Ik word gepest - with user",
//                Ip = "244.255.63.39",
//                Location = "Arco Baleno - with user",
//                Time = DateTime.UtcNow,
//                Hidden = false,
//                Type = "Pesten",
//                UserAgent = "Opera",
//                Guid = Guid.NewGuid().ToString()
//            });

//            var reportId = _reportProxy.GetReports().Where(r => r.Guid == report.Guid).FirstOrDefault().Id;
//            _contactProxy.AddContact(new Contact
//            {
//                EmailAddress = "beyonce@gijs.nl",
//                Name = "Gijs Jannssenn",
//                PhoneNumber = "0655889944",
//                Report = reportId
//            });

//            report = _reportProxy.AddReport(new WebApi.Report
//            {
//                Created = DateTime.UtcNow,
//                Status = StatusName.Open,
//                Description = "Gijs pest mij",
//                Ip = "190.11.22.86",
//                Location = "Azurro",
//                Time = DateTime.UtcNow.AddHours(4),
//                Hidden = false,
//                Type = "Pesten",
//                UserAgent = "Opera",
//                Guid = Guid.NewGuid().ToString()
//            });

//            report = _reportProxy.AddReport(new WebApi.Report
//            {
//                Created = DateTime.UtcNow,
//                Status = StatusName.Open,
//                Description = "Ik weer word gepest!",
//                Ip = "244.255.63.39",
//                Location = "Arco Baleno",
//                Time = DateTime.UtcNow.AddHours(7),
//                Hidden = false,
//                Type = "Pesten",
//                UserAgent = "Opera",
//                Guid = Guid.NewGuid().ToString()
//            });

//            report = _reportProxy.AddReport(new WebApi.Report
//            {
//                Created = DateTime.UtcNow,
//                Status = StatusName.Open,
//                Description = "Er word drugs gedealed",
//                Ip = "180.16.52.39",
//                Location = "Arco Baleno",
//                Time = DateTime.UtcNow.AddHours(8),
//                Hidden = false,
//                Type = "Drugs",
//                UserAgent = "Opera",
//                Guid = Guid.NewGuid().ToString() //,
//                //Contacts = contact
//            });

//            report = _reportProxy.AddReport(new WebApi.Report
//            {
//                Created = DateTime.UtcNow,
//                Status = StatusName.InProgress,
//                Description = "Wiet verkoop",
//                Ip = "177.75.22.11",
//                Location = "Sportcentrum",
//                Time = DateTime.UtcNow.AddHours(3).AddMinutes(34),
//                Hidden = false,
//                Type = "Drugs",
//                UserAgent = "Chrome",
//                Guid = Guid.NewGuid().ToString()
//            });

//            report = _reportProxy.AddReport(new WebApi.Report
//            {
//                Created = DateTime.UtcNow,
//                Status = StatusName.Open,
//                Description = "Rook gesignaleerd",
//                Ip = "10.180.14.6",
//                Location = "Ocra",
//                Time = DateTime.UtcNow.AddHours(7).AddMinutes(21),
//                Hidden = false,
//                Type = "Brand",
//                UserAgent = "Chrome",
//                Guid = Guid.NewGuid().ToString()
//            });

//            report = _reportProxy.AddReport(new WebApi.Report
//            {
//                Created = DateTime.UtcNow,
//                Status = StatusName.Open,
//                Description = "Gebroken raam, verdwenen computer",
//                Ip = "10.180.14.47",
//                Location = "Bianco begane grond",
//                Time = DateTime.UtcNow.AddHours(11).AddMinutes(30),
//                Hidden = false,
//                Type = "Diefstal",
//                UserAgent = "Chrome",
//                Guid = Guid.NewGuid().ToString()
//            });

//            return RedirectToAction("Index");
//        }

//        private List<LogBookEntry> AddRemarksToLogbook(IQueryable<WebApi.Remark> remarks)
//        {
//            List<LogBookEntry> result = new List<LogBookEntry>();
//            foreach (var remark in remarks)
//            {
//                result.Add(new LogBookEntry
//                {
//                    Id = (remark.Id * 13),
//                    Created = remark.Created,
//                    Description = remark.Description,
//                    User = remark.User
//                });
//            }
//            return result;
//        }

//        private List<LogBookEntry> AddStatussesToLogbook(IQueryable<Status> statusses, WebApi.Report report)
//        {
//            List<LogBookEntry> result = new List<LogBookEntry>();
//            Status lastStatus = null;

//            foreach (var status in statusses)
//            {
//                var description = CreateLogbookStatusDescription(status, report.Id, lastStatus);
//                lastStatus = status;

//                result.Add(new LogBookEntry
//                {
//                    Id = (status.Id * 10),
//                    Created = status.Created,
//                    Description = description,
//                    User = status.User
//                });
//            }
//            return result;
//        }

//        private string CreateLogbookStatusDescription(Status status, int reportId, Status lastStatus = null)
//        {
//            var description = "";
//            if (lastStatus == null)
//            {
//                var person = "Anoniem";
//                var contact = _contactProxy.GetContacts().Where(c => c.Report == reportId).FirstOrDefault();
//                if (contact != null)
//                {
//                    person = contact.Name;
//                }
//                // TODO: contacts
//                //if (report.Contacts.Count > 0)
//                //{
//                //    person = report.Contacts[0].Name;
//                //}
//                description = string.Format("Melding is aangemaakt door: {0} met de status {1}.", person,
//                    status.Name.GetStatusDisplayNameFromMetadata());
//            }
//            else
//            {
//                description = string.Format("The Status {0} is changed to {1}.", lastStatus.Name.GetStatusDisplayNameFromMetadata(),
//                    status.Name.GetStatusDisplayNameFromMetadata());
//            }
//            return description;
//        }

//        [HttpPost]
//        public ActionResult Search(SearchModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                throw new NotImplementedException("No error handling for invalid search model has been implemented!");
//            }

//            if (string.IsNullOrWhiteSpace(model.SearchText))
//            {
//                return RedirectToAction("Index");
//            }

//            Session["search_text"] = model.SearchText;

//            return RedirectToAction("Search");
//        }

//        [HttpGet]
//        public ActionResult Search(int id = 0, string sortBy = DefaultSortBy)
//        {
//            var sessionTimeOut = Session.Timeout = 60;
//            if (Session["user"] == null || sessionTimeOut == 0)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            // If we don't have a session search query, just go to index
//            if (Session["search_text"] == null)
//            {
//                return RedirectToAction("Index");
//            }

//            int page = 0;
//            if (id > 0)
//            {
//                page = id - 1;
//            }
//            int items = DefaultItems;


//            // SearchText holds the text given from the view.
//            var searchText = (string)Session["search_text"];
//            ViewBag.SearchText = searchText;

//            IQueryable<Report> reports;

//            if (!(bool)Session["is_admin"])
//            {
//                reports = _reportProxy.GetReports()
//                    .Where(r => r.Status != StatusName.Solved)
//                    .Where(report => report.Hidden == false);
//            }
//            else
//            {
//                reports = _reportProxy.GetReports();
//            }

//            ViewBag.pages = (int)Math.Ceiling((double)reports.Count() / (double)items);
//            ViewBag.currentPage = page;

//            // This is the best way I could find to do status searching with localized strings.
//            // I'm so so sorry for this atrocety.
//            var statusDict = new Dictionary<string, StatusName>
//            {
//                {DisplayNames.StatusInProgress.ToLower(), StatusName.InProgress},
//                {DisplayNames.StatusOpen.ToLower(), StatusName.Open},
//                {DisplayNames.StatusSolved.ToLower(), StatusName.Solved},
//                {DisplayNames.StatusTransferred.ToLower(), StatusName.Transferred}
//            };
//            StatusName searchStatus;
//            var foundSearchStatus = statusDict.TryGetValue(searchText.ToLower(), out searchStatus);

//            // Prevent searching for the king james bible
//            if (searchText.Length > 500)
//            {
//                ModelState.AddModelError("", "Kan niet zoeken voor meer dan 500 karakters.");
//                return View("Index", new List<Report>());
//            }

//            // And now for the hack to get datetimes working for now, this will have to be improved
//            // to be a more full featured search query system later on.
//            int searchNumber;
//            var isNumber = int.TryParse(searchText, out searchNumber);

//            // Filter by the search query
//            reports = reports.Where(r => r.Description.Contains(searchText) || r.Type.Contains(searchText) || r.Location.Contains(searchText) ||
//                (foundSearchStatus && r.Status == searchStatus) ||

//                // Anything for which the search needs to be a number
//                (isNumber && (
//                // Created and Time
//                    r.Created.Year == searchNumber ||
//                    r.Created.Month == searchNumber ||
//                    r.Created.Day == searchNumber ||
//                    r.Time.Year == searchNumber ||
//                    r.Time.Month == searchNumber ||
//                    r.Time.Day == searchNumber
//                )));

//            // Collapse it into a list
//            List<Report> reportsList;
//            try
//            {
//                ViewBag.SortingBy = sortBy;
//                reportsList = reports.SortBy(sortBy).ToList();
//            }
//            catch (ArgumentException)
//            {
//                // sortBy was invalid, use the default
//                ViewBag.SortingBy = DefaultSortBy;
//                reportsList = reports.SortBy(DefaultSortBy).ToList();
//            }

//            if (reportsList.Count == 0)
//            {
//                ModelState.AddModelError("", "Geen resultaten gevonden.");
//            }

//            return View("Index", reportsList.Skip(items * page).Take(items));
//        }

//        private const string DefaultSortBy = "Created DESC";
//        private const int DefaultItems = 15;

//        private readonly ContactProxy _contactProxy;
//        private readonly RemarkProxy _remarkProxy;
//        private readonly ReportProxy _reportProxy;
//        private readonly StatusProxy _statusProxy;
//    }
//}
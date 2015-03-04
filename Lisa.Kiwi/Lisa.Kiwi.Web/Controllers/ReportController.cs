using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.Web.App_GlobalResources.Resources;
using Lisa.Kiwi.Web.Models;
using Lisa.Kiwi.WebApi;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lisa.Kiwi.Web.Reporting.Controllers
{
	public class ReportController : Controller
	{
        public ActionResult Index()
        {
            return View(new CategoryViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(CategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = _modelFactory.Create(viewModel);
            report = await _reportProxy.PostAsync(report);

            var cookie = new HttpCookie("report", report.Id.ToString());
            Response.SetCookie(cookie);

            return RedirectToAction("Location");
        }

        public ActionResult Location()
        {
            return View(new LocationViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Location(LocationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var cookie = Request.Cookies["report"];
            int reportId = Int32.Parse(cookie.Value);

            var report = await _reportProxy.GetAsync(reportId);
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(reportId, report);

            return RedirectToAction("Done");
        }

        public ActionResult Done()
        {
            return View();
        }

		public ActionResult Type()
		{
            var reportTypes = GetReportTypes();
			ViewData["reportType"] = reportTypes;

			return View();
		}

        //Staph right here

		[HttpPost]
        [ValidateInput(false)]
		public ActionResult Type(string reportType)
		{
			if (!ModelState.IsValid)
			{
                return View();
            }
			
            CloudTable table = GetTableStorage();
            OriginalReport report = DefineReport(reportType);
			TableOperation insertOperation = TableOperation.Insert(report);
			table.Execute(insertOperation);

			// Cookie stays alive until user closes browser
			HttpCookie userReport = new HttpCookie("userReport");
			userReport["guid"] = report.Guid;
			Response.Cookies.Add(userReport);

            string controller;
            switch(reportType) {
                //case "Drugs":
                //    controller = "Drugs";
                //    break;
                default:
                    controller = null;
                    break;
            }

            if (controller != null)
            {
                return RedirectToAction("Details", controller);
            }
            return RedirectToAction("Details");			
		}

		public ActionResult Details()
		{
            HttpCookie cookie = HttpContext.Request.Cookies["userReport"];
            string guid = cookie.Values["guid"];

            ViewData["buildings"] = new List<SelectListItem>
            {
                new SelectListItem { Text = "" , Value = "" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Buiten , Value = "Buiten" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Azurro , Value = "Azurro" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Romboutslaan , Value = "Romboutslaan" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Syndion , Value = "Syndion" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Samenwerkingsgebouw , Value = "Samenwerkingsgebouw" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Drechsteden_College , Value = "Drechsteden College" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Appartementen , Value = "Appartementen" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Brandweerkazerne , Value = "Brandweerkazerne" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Lilla , Value = "Lilla" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Marrone , Value = "Marrone" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Rosa , Value = "Rosa" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Verde , Value = "Verde" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Giallo , Value = "Giallo" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Indaco , Value = "Indaco" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Bianco , Value = "Bianco" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Orca , Value = "Orca" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Arcobaleno , Value = "Arcobaleno" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Celeste , Value = "Celeste" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Duurzaamheidsfabriek , Value = "Duurzaamheidsfabriek" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Betaalde_parkeerplaats , Value = "Betaalde parkeerplaats" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Schippersinternaat , Value = "Schippersinternaat" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Sporthal , Value = "Sporthal" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Bogermanschool , Value = "Bogermanschool" },
                new SelectListItem { Text = DisplayNames.ReportController_Details_Wartburg_College , Value = "Wartburg College" },
            };

            CloudTable table = GetTableStorage();
            TableOperation retrieveOperation = TableOperation.Retrieve<OriginalReport>(guid, "");
            TableResult retrievedResult = table.Execute(retrieveOperation);
            OriginalReport entity = (OriginalReport)retrievedResult.Result;

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

			var report = new OriginalReport {Time = DateTime.Now};
            return View(report);
		}

		[HttpPost]
        [ValidateInput(false)]
        public ActionResult Details(OriginalReport data, string buildings)
		{
		    data.Building = buildings;

			HttpCookie cookie = HttpContext.Request.Cookies["userReport"];
			string guid = cookie.Values["guid"];

			if (!ModelState.IsValid)
			{
                return View(data);
            }

            int timeZoneDifference = 0;
            if (data.Offset < 0)
            {
                timeZoneDifference = data.Offset / 60;
                data.Time = data.Time.AddHours(timeZoneDifference);
            }
            else
            {
                timeZoneDifference = data.Offset / 60;
                data.Time = data.Time.AddHours(timeZoneDifference);
            }
			
            CloudTable table = GetTableStorage();
			TableOperation retrieveOperation = TableOperation.Retrieve<OriginalReport>(guid, "");
			TableResult retrievedResult = table.Execute(retrieveOperation);
			OriginalReport updateEntity = (OriginalReport) retrievedResult.Result;

            if (updateEntity == null)
            {
                return RedirectToAction("Index");
            }

            UpdateDetails(updateEntity, data, table);

            return RedirectToAction("ContactDetails");
		}

		public ActionResult ContactDetails()
		{
            HttpCookie cookie = HttpContext.Request.Cookies["userReport"];
            string guid = cookie.Values["guid"];

            CloudTable table = GetTableStorage();
            TableOperation retrieveOperation = TableOperation.Retrieve<OriginalReport>(guid, "");
            TableResult retrievedResult = table.Execute(retrieveOperation);
            OriginalReport entity = (OriginalReport)retrievedResult.Result;

            if(entity == null)
            {
    			return RedirectToAction("Index");
            }

            return View();
        }

		[HttpPost]
        [ValidateInput(false)]
		public async Task<ActionResult> ContactDetails(ContactMetadata data)
		{
			HttpCookie cookie = HttpContext.Request.Cookies["userReport"];
			string guid = cookie.Values["guid"];

			// Make sure that if a checkmark was checked but no value is given it gives an error
			// TODO: Localize me
			if (data != null &&
				((data.UseName && data.Name == null) ||
				 (data.UsePhoneNumber && data.PhoneNumber == null) ||
				 (data.UseEmail && data.Email == null) 
				 ))
			{
				ModelState.AddModelError("", "Een geselecteerd veld mag niet leeg zijn, deselecteer het veld als u niks wenst in te voeren.");
			}

			if (!ModelState.IsValid)
			{
				return View(data);
			}

			CloudTable table = GetTableStorage();
			TableOperation retrieveOperation = TableOperation.Retrieve<OriginalReport>(guid, "");
			TableResult retrievedResult = table.Execute(retrieveOperation);
			OriginalReport entity = (OriginalReport) retrievedResult.Result;

			if (entity == null)
			{
				return RedirectToAction("Index");
			}

			var report = new Report
			{
				Description = entity.Description,
				Created = entity.Created,
				Location = entity.Location,
                Category = entity.Type
			};

			var reportEntity = await _reportProxy.PostAsync(report);
			if (reportEntity == null)
			{
				return View();
			}

			if (data.Name != null || data.Email != null || data.PhoneNumber != null)
			{
				var newContact = await CreateContact(data, reportEntity.Id, new Guid());
				var entityContact = new ContactMetadata
				{
					Id = newContact.Id,
					Name = newContact.Name,
					Email = newContact.EmailAddress,
					PhoneNumber = newContact.PhoneNumber,
					Report = newContact.Report,
					PartitionKey = entity.PartitionKey,
					RowKey = ""
				};

				CloudTable tableContact = GetContactTableStorage();
				TableOperation insertOperation = TableOperation.Insert(entityContact);
				tableContact.Execute(insertOperation);
			}

			return RedirectToAction("Confirmed", "Report");
		}

		public ActionResult Confirmed()
		{
            HttpCookie cookie = HttpContext.Request.Cookies["userReport"];
            string guid = cookie.Values["guid"];

            CloudTable table = GetTableStorage();
            TableOperation retrieveOperation = TableOperation.Retrieve<OriginalReport>(guid, "");
            TableResult retrievedResult = table.Execute(retrieveOperation);
            OriginalReport entity = (OriginalReport)retrievedResult.Result;

            if (entity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(entity);
                table.Execute(deleteOperation);
            }

            CloudTable tableContact = GetContactTableStorage();
            retrieveOperation = TableOperation.Retrieve<ContactMetadata>(guid, "");
            retrievedResult = tableContact.Execute(retrieveOperation);
            ContactMetadata contact = (ContactMetadata)retrievedResult.Result;

            if (contact != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(contact);
                tableContact.Execute(deleteOperation);
            }

            ViewBag.Contact = contact;
			return View(entity);
		}

        private List<SelectListItem> GetReportTypes()
        {

            List<SelectListItem> reportTypes = new List<SelectListItem>();

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeEHBO,
                Value = "EHBO"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeFighting,
                Value = "Vechtpartij"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeDrugs,
                Value = "Drugs"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeTheft,
                Value = "Diefstal"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeBullying,
                Value = "Pesten"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeDestruction,
                Value = "Vernieling"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeMisc,
                Value = "Overig"
            });

            return reportTypes;
        }

        private OriginalReport DefineReport(string reportType)
        {
            OriginalReport report = new OriginalReport();
            var guid = Guid.NewGuid().ToString();

            report.Guid = guid;
            report.Time = DateTime.UtcNow;
            report.Type = reportType.ToString();
            report.PartitionKey = guid;
            report.RowKey = "";

            return report;
        }

        private void UpdateDetails(OriginalReport updateEntity, OriginalReport data, CloudTable table)
        {
            if (string.IsNullOrEmpty(data.Building))
            {
                updateEntity.Location = data.Location;
            }
            else
            {
                updateEntity.Location = data.Building + " - " + data.Location;
            }

            updateEntity.Time = data.Time;
            updateEntity.Description = data.Description;

            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(updateEntity);
            table.Execute(insertOrReplaceOperation);
        }

        private async Task<Contact> CreateContact(ContactMetadata data, int id, Guid editToken) 
        {
            var contact = new Contact
            {
                Name = data.Name,
                EmailAddress = data.Email,
                PhoneNumber = data.PhoneNumber,
                EditToken = editToken,
                Report = id
            };
            await _contactProxy.PostAsync(contact);
            return contact;
        }

        private CloudTable GetTableStorage()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("report");
            table.CreateIfNotExists();

            return table;
        }

        private CloudTable GetContactTableStorage()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable tableContact = tableClient.GetTableReference("contact");
            tableContact.CreateIfNotExists();

            return tableContact;
        }

        private readonly Proxy<Contact> _contactProxy = new Proxy<Contact>("http://localhost:20151", "/contacts/");
        //Changed localhost.fiddler:20151 to localhost:20151 just in case you were wondering. 
        private readonly Proxy<Report> _reportProxy = new Proxy<Report>("http://localhost:20151/", "/reports/");
        private readonly Proxy<WebApi.Remark> _remarkProxy = new Proxy<WebApi.Remark>("http://localhost:20151", "/remarks/");
        private readonly ModelFactory _modelFactory = new ModelFactory();
	}
}
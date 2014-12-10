using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

// Onderstaande Data referentie klopt wel, want deze namespace word gehandhaafd door de Odata Client.
using Lisa.Kiwi.Data;

using Lisa.Kiwi.Web.Reporting.Models;
using Lisa.Kiwi.Web.Reporting.Utils;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Resources;

namespace Lisa.Kiwi.Web.Reporting.Controllers
{
	public class ReportController : Controller
	{
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Type()
		{
            var reportTypes = GetReportTypes();
			ViewData["reportType"] = reportTypes;

			return View();
		}

		[HttpPost]
        [ValidateInput(false)]
		public ActionResult Type(string reportType)
		{
			if (ModelState.IsValid)
			{
				CloudTable table = GetTableStorage();
                OriginalReport report = DefineReport(reportType);
				TableOperation insertOperation = TableOperation.Insert(report);
				table.Execute(insertOperation);

				// Cookie stays alive until user closes browser
				HttpCookie userReport = new HttpCookie("userReport");
				userReport["guid"] = report.Guid;
				Response.Cookies.Add(userReport);

				return RedirectToAction("Details", "Report");
			}
			return View();
		}

		public ActionResult Details()
		{
            HttpCookie cookie = HttpContext.Request.Cookies["userReport"];
            string guid = cookie.Values["guid"];

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
		public ActionResult Details(OriginalReport data)
		{
			HttpCookie cookie = HttpContext.Request.Cookies["userReport"];
			string guid = cookie.Values["guid"];

			if (!ModelState.IsValid)
			{
                return View(data);
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

            return RedirectToAction("ContactDetails", "Report");
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
				 (data.UseEmail && data.Email == null) ||
				 (data.UseStudentNumber && data.StudentNumber == null)))
			{
				ModelState.AddModelError("", "Een geselecteerd veld mag niet leeg zijn, deselecteer het veld als u niks w");
				return View(data);
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

			var report = new WebApi.Report
			{
				Description = entity.Description,
				Created = entity.Created,
				Location = entity.Location,
				Time = entity.Time,
				Guid = entity.PartitionKey,
				Type = entity.Type,

				Ip = Request.UserHostAddress,
				UserAgent = Request.UserAgent
			};

			var reportEntity = await _reportProxy.AddManualReport(report);
			if (reportEntity == null)
			{
				return View();
			}

			if (data.Name != null || data.Email != null || data.PhoneNumber != null || data.StudentNumber != null)
			{
				var newContact = CreateContact(data, reportEntity.Id, reportEntity.EditToken);
				var entityContact = new ContactMetadata
				{
					Id = newContact.Id,
					Name = newContact.Name,
					Email = newContact.EmailAddress,
					PhoneNumber = newContact.PhoneNumber,
					StudentNumber = newContact.StudentNumber,
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
                Text = DisplayNames.ReportTypeDrugs,
                Value = "Drugs"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeNuisance,
                Value = "Overlast"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeTheft,
                Value = "Diefstal"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeFire,
                Value = "Brand"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeBurglary,
                Value = "Inbraak"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeDigital,
                Value = "Digitaal"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeBullying,
                Value = "Pesten"
            });

            reportTypes.Add(new SelectListItem
            {
                Text = DisplayNames.ReportTypeVehicles,
                Value = "Voertuigen"
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
            updateEntity.Location = data.Location;
            updateEntity.Time = data.Time;
            updateEntity.Description = data.Description;

            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(updateEntity);
            table.Execute(insertOrReplaceOperation);
        }

        private Contact CreateContact(ContactMetadata data, int id, Guid editToken) 
        {
            var contact = new Contact
            {
                Name = data.Name,
                EmailAddress = data.Email,
                PhoneNumber = data.PhoneNumber,
                StudentNumber = data.StudentNumber,
                EditToken = editToken,
                Report = id
            };
            _contactProxy.AddContact(contact);
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

		private readonly ReportProxy _reportProxy = new ReportProxy(ConfigHelper.GetODataUri());
        private readonly ContactProxy _contactProxy = new ContactProxy(ConfigHelper.GetODataUri());
		private readonly StatusProxy _statusProxy = new StatusProxy(ConfigHelper.GetODataUri());
	}
}
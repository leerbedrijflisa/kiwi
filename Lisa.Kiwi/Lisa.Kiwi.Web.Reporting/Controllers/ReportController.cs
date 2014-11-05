using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Linq;
using Lisa.Kiwi.WebApi.Access;
using Lisa.Kiwi.Data;
using Lisa.Kiwi.Web.Reporting.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure;
using Lisa.Kiwi.Web.Reporting.Utils;
using System.Collections.Generic;

namespace Lisa.Kiwi.Web.Reporting.Controllers
{
    public class ReportController : Controller
    {
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Type()
        {
            var types = Enum.GetValues(typeof(ReportType)).Cast<ReportType>();
            List<SelectListItem> reportTypes = new List<SelectListItem>();
            foreach (var reportType in types)
            {
                reportTypes.Add(new SelectListItem
                {
                    Text = reportType.GetReportTypeDisplayNameFromMetadata(),
                    Value = reportType.ToString()
                });
            }


            ViewData["reportType"] = reportTypes;


            return View();
        }

        [HttpPost]
        public ActionResult Type(ReportType reportType)
        {
            if (ModelState.IsValid)
            {
                CloudTable table = GetTableStorage();

                string guid = Guid.NewGuid().ToString();

                OriginalReport report = new OriginalReport();
                
                report.Type = reportType;
                report.Guid = guid;
                report.Time = DateTime.UtcNow;

                report.PartitionKey = guid;
                report.RowKey = "";

                // TODO: report type cant use enums

                TableOperation insertOperation = TableOperation.Insert(report);
                table.Execute(insertOperation);

                // Cookie stays alive until user closes browser
                HttpCookie userReport = new HttpCookie("userReport");
                userReport["guid"] = guid;
                Response.Cookies.Add(userReport);

                return RedirectToAction("Details", "Report");
            }
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(OriginalReport data)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["userReport"];
            string guid = cookie.Values["guid"];

            if (ModelState.IsValid)
            {
                CloudTable table = GetTableStorage();

                TableOperation retrieveOperation = TableOperation.Retrieve<OriginalReport>(guid, "");
                TableResult retrievedResult = table.Execute(retrieveOperation);

                OriginalReport updateEntity = (OriginalReport)retrievedResult.Result;

                if(retrievedResult.Result != null)
                {
                    updateEntity.Location = data.Location;
                    updateEntity.Time = data.Time;
                    updateEntity.Description = data.Description;

                    TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(updateEntity);
                    table.Execute(insertOrReplaceOperation);

                    return RedirectToAction("ContactDetails", "Report");
                }
            }
            return View();
        }

        public ActionResult ContactDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contactdetails(ContactMetadata data)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["userReport"];
            string guid = cookie.Values["guid"];

            if (ModelState.IsValid)
            {
                CloudTable table = GetTableStorage();

                TableOperation retrieveOperation = TableOperation.Retrieve<OriginalReport>(guid, "");
                TableResult retrievedResult = table.Execute(retrieveOperation);

                OriginalReport entity = (OriginalReport)retrievedResult.Result;

                if (retrievedResult != null)
                {
                    var report = new WebApi.Report
                    {
                        Description = entity.Description,
                        Created = entity.Created,
                        Location = entity.Location,
                        Time = entity.Time,
                        Guid = entity.PartitionKey,
                        Type = entity.Type
                    };

                    _reportProxy.AddReport(report);

                    var reportEntity = _reportProxy.GetReports();
                    var getReport = reportEntity.Where(r => r.Guid == guid).FirstOrDefault();

                 
                    if(getReport != null)
                    {
                        var status = new WebApi.Status
                        {
                            Created = entity.Created,
                            Name = StatusName.Open,
                            Report = getReport.Id
                        };

                        _statusProxy.AddStatus(status);
                    }


                    return RedirectToAction("Confirmed", "Report");
                }
            }
            return View();
        }

        public ActionResult Confirmed()
        {
            return View();
        }

		private readonly ReportProxy _reportProxy = new ReportProxy(ConfigHelper.GetODataUri());
		private readonly StatusProxy _statusProxy = new StatusProxy(ConfigHelper.GetODataUri());
    }
}
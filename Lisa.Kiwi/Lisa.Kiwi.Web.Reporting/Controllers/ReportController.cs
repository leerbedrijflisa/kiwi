using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using Lisa.Kiwi.Web.Reporting.Models;
using Lisa.Kiwi.Tools;
using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using Microsoft.WindowsAzure;

namespace Lisa.Kiwi.Web.Reporting.Controllers
{
    public class ReportController : Controller
    {
        CloudTable table;

        public ReportController()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference("report");
            table.CreateIfNotExists();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Type()
        {
            var types = new string[]
            {
                "Drugs",
                "Overlast",
                "Voertuigen",
                "Inbraak",
                "Diefstal",
                "Intimidatie",
                "Pesten",
                "Digitaal grensoverschrijdend gedrag",
                "Etc"
            };

            ViewBag.ReportTypes = new SelectList(types);

            return View();
        }

        [HttpPost]
        public ActionResult Type(string reportType)
        {
            if (reportType != null)
            {
                if (ModelState.IsValid)
                {
                    string guid = Guid.NewGuid().ToString();

                    OriginalReport report = new OriginalReport();
                
                    report.Type = reportType;
                    report.Guid = guid;
                    report.Time = DateTime.Now;

                    report.PartitionKey = guid;
                    report.RowKey = "";

                    TableOperation insertOperation = TableOperation.Insert(report);
                    table.Execute(insertOperation);

                    HttpCookie userReport = new HttpCookie("Cookie");
                    userReport["guid"] = guid;

                    return RedirectToAction("Details", "Report");
                }
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
            HttpCookie cookie = HttpContext.Request.Cookies.Get("Cookie");

            

            if (ModelState.IsValid)
            {


                return RedirectToAction("ContactDetails", "Report");
            }

            return View();
        }

        public ActionResult ContactDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contactdetails(Contact data, string guid)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact();
                contact.Name = data.Name;
                contact.PhoneNumber = data.PhoneNumber;
                contact.Email = data.Email;
                contact.StudentNumber = data.StudentNumber;
                return RedirectToAction("Confirmed", "Report");
            }

            return View();
        }

        public ActionResult Confirmed()
        {
            return View();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Default;

namespace Lisa.Kiwi.WebApi.Access
{
    public class ReportProxy
    {
        // You need this to initialize the access layer
        private static readonly Container container = new Container(new Uri(ClientConfig.BaseUrl));

        // Get an entire entity set.
        public static IQueryable<Report> GetReports()
        {
            return container.Report;
        }

        //Get an entity by keyvalue
        public static void GetReport(string guid)
        {
            var report = container.Report.ByKey(new Dictionary<string, object> {{"ID", 1}}).GetValue();
        }

        public static IEnumerable GetAllReportsByStatusIsNot(string status)
        {
            var report = container.Report.AppendRequestUri("$filter=Status ne " + status).ToList();

            return report;
        }


        //Create a new entity
        static void AddReport(Report report)
        {
            container.AddToReport(report);
            var serviceResponse = container.SaveChanges();
        }
    }
}

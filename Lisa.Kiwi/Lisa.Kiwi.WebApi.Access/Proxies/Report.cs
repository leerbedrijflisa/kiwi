using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Default;
using Lisa.Kiwi.WebApi.Models;

/* 
 * 
 *  This is an EXAMPLE.
 * 
 * 
 *  And an example ONLY.
 * 
 */

namespace Lisa.Kiwi.WebApi.Access.Proxies
{
    public class Report
    {
        // You need this to initialize the access layer
        private static readonly Container container = new Container(new Uri(ClientConfig.BaseUrl));

        // Get an entire entity set.
        public static IEnumerable GetAllReports()
        {
            var reportsList = container.Report.ToList();

            return reportsList;
        }

        //Get an entity by keyvalue
        public static void GetReport(string guid)
        {
            //Lisa.Kiwi.WebApi.Models.Report
            var report = container.Report.ByKey(new Dictionary<string, object> {{"ID", 1}}).GetValue();
        }

        public static IEnumerable GetAllReportsByStatusIsNot(string status)
        {
            //Lisa.Kiwi.WebApi.Models.Report
            var report = container.Report.AppendRequestUri("$filter=Status ne " + status).ToList();

            return report;
        }


        //Create a new entity
        static void AddReport(WebApi.Models.Report report)
        {
            container.AddToReport(report);
            var serviceResponse = container.SaveChanges();
        }
    }
}

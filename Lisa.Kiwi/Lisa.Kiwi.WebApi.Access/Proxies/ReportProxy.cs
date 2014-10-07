using System;
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

        //Create a new entity
        static void AddReport(Report report)
        {
            container.AddToReport(report);
            var serviceResponse = container.SaveChanges();
        }
    }
}

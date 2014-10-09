using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class ReportProxy
    {
        // Get an entire entity set.
        public static IQueryable<Report> GetReports()
        {
            return Client.container.Report;
        }

        //Create a new entity
        public static void AddReport(Report report)
        {
            Client.container.AddToReport(report);
            Client.container.SaveChanges();
            Client.container = new Container(Client.BaseUri);
        }
    }
}

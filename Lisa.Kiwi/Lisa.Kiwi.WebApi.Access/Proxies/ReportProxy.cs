using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class ReportProxy
    {
        // Get an entire entity set.
        public static IQueryable<Report> GetReports()
        {
            return Client.Container.Report;
        }

        //Create a new entity
        public static void AddReport(Report report)
        {
            Client.Container.AddToReport(report);
            Client.Container.SaveChanges();
            Client.Container = new Container(Client.BaseUri);
        }
    }
}

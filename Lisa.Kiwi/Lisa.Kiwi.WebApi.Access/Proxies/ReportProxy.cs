using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class ReportProxy : Client
    {
        // Get an entire entity set.
        public IQueryable<Report> GetReports()
        {
            return Container.Report;
        }

        //Create a new entity
        public void AddReport(Report report)
        {
            Container.AddToReport(report);
            Container.SaveChanges();
        }
    }
}

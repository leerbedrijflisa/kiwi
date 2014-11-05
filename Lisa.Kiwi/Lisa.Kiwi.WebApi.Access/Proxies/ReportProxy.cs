using System.Linq;

namespace Lisa.Kiwi.WebApi.Access
{
    public class ReportProxy : Client
    {
        // Get an entire entity set.
        public IQueryable<Report> GetReports(bool getContacts = false)
        {
            if (!getContacts)
            {
                return Container.Report;
            }
            else
            {
                return Container.Report.Expand(r => r.Contacts);
            }
        }

        //Create a new entity
        public void AddReport(Report report)
        {
            Container.AddToReport(report);
            Container.SaveChanges();
        }
    }
}

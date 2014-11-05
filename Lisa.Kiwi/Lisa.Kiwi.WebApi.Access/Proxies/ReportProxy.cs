using System.Linq;
using Default;

namespace Lisa.Kiwi.WebApi.Access
{
    public class ReportProxy
	{
		private readonly Container _container = new Container(Client.BaseUri);

        // Get an entire entity set.
        public IQueryable<Report> GetReports(bool getContacts = false)
        {
            if (!getContacts)
            {
				return _container.Report;
            }
            else
            {
				return _container.Report.Expand(r => r.Contacts);
            }
        }

        //Create a new entity
        public void AddReport(Report report)
        {
			_container.AddToReport(report);
			_container.SaveChanges();
        }
    }
}

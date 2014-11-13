using System;
using System.Linq;
using Default;
using Microsoft.OData.Client;

namespace Lisa.Kiwi.WebApi.Access
{
	public class ReportProxy
	{
		public ReportProxy(Uri odataUrl)
		{
            _container = new AuthenticationContainer(odataUrl);
        }

		// Get an entire entity set.
		public IQueryable<Report> GetReports(ExpandOptions options = ExpandOptions.DontExpand)
		{
            switch (options) {
                case ExpandOptions.ExpandContacts:
                {
                    return _container.Report.Expand(r => r.Contacts); 
                }
                default:
                {
                    return _container.Report; 
                }

            }
		}

		//Create a new entity
		public void AddReport(Report report)
		{
			_container.AddToReport(report);
			_container.SaveChanges();
		}

	    public void SaveReport(Report report)
	    {
            _container.ChangeState(report, EntityStates.Modified);
	        _container.SaveChanges();
	    }

		private readonly Container _container;
	}
}
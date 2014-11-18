using System;
using System.Linq;
using Default;
using Microsoft.OData.Client;
using Lisa.Kiwi.WebApi.Access;

namespace Lisa.Kiwi.WebApi.Access
{
	public class ReportProxy
	{
		public ReportProxy(Uri odataUrl)
		{
			_container = new Container(odataUrl);
		}

		// Get an entire entity set.
		public IQueryable<Report> GetReports()
		{
            return _container.Report;             
		}

		//Create a new entity
		public Report AddReport(Report report)
		{
			_container.AddToReport(report);
			_container.SaveChanges();

            return report;
		}

	    public void SaveReport(Report report)
	    {
            _container.ChangeState(report, EntityStates.Modified);
	        _container.SaveChanges();
	    }

		private readonly Container _container;
	}
}
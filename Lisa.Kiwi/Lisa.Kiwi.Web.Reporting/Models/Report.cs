using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lisa.Kiwi.Web.Reporting.Models
{
	public class Report : TableEntity
	{
		public Report()
		{
			CreatedAt = DateTime.UtcNow;
		}

		public int Id { get; set; }
	    public string Type { get; set; }
		public string Building { get; set; }
        public string DetailedLocation { get; set; }
		public DateTime CreatedAt { get; set; }
        public string ReporterName { get; set; }
        public string ReporterTelephoneNumber { get; set; }
        public string ReporterEmail { get; set; }
        public string ReporterComment { get; set; }
	}
}
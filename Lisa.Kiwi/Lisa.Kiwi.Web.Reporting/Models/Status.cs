using System;

namespace Lisa.Kiwi.Web.Reporting.Models
{
	public class Status
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Created { get; set; }
		public OriginalReport Report { get; set; }
	}
}
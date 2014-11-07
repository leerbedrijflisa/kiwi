using System;

namespace Lisa.Kiwi.Web.Dashboard.Models
{
	public class LogBookEntry
	{
		public int Id { get; set; }
		public string User { get; set; }
		public string Description { get; set; }
		public DateTimeOffset Created { get; set; }
	}
}
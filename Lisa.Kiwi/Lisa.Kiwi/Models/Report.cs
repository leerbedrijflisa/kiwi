using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.WebApi
{
	public class Report
	{
		[Key]
		public int Id { get; set; }

		public string Description { get; set; }
		public DateTimeOffset Created { get; set; }
		public string Location { get; set; }
		public DateTimeOffset Time { get; set; }
		public bool Hidden { get; set; }
		public string Guid { get; set; }
		public string UserAgent { get; set; }
		public string Ip { get; set; }
		public StatusName Status { get; set; }
		public ReportType Type { get; set; }
		public IEnumerable<Contact> Contacts { get; set; }
	}
}
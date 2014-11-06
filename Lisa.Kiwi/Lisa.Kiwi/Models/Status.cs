using System;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.WebApi
{
	public class Status
	{
		public int Id { get; set; }
		public DateTimeOffset Created { get; set; }
		public StatusName Name { get; set; }
		public int Report { get; set; }
		public bool Visible { get; set; }
	}
}
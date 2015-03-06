using System;

namespace Lisa.Kiwi.Web.Models
{
	public class Remark
	{
		public Remark()
		{
			Created = DateTime.UtcNow;
		}

		public int Id { get; set; }
		public string Description { get; set; }
		public DateTime Created { get; set; }
		public OriginalReport Report { get; set; }
	}
}
using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lisa.Kiwi.Data
{
	public class Status
	{
		public int Id { get; set; }
		public DateTimeOffset Created { get; set; }
		public StatusName Name { get; set; }
        public virtual IdentityUser User { get; set; }
		public virtual Report Report { get; set; }
		public bool Visible { get; set; }
	}
}
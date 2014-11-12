using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lisa.Kiwi.Data
{
	public class KiwiContext : IdentityDbContext<IdentityUser>
	{
		public DbSet<Report> Reports { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<Remark> Remarks { get; set; }
		public DbSet<Contact> Contacts { get; set; }
	}
}
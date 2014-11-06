using System.Data.Entity;

namespace Lisa.Kiwi.Data
{
	public class KiwiContext : DbContext
	{
		public DbSet<Report> Reports { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<Remark> Remarks { get; set; }
		public DbSet<Contact> Contacts { get; set; }
	}
}
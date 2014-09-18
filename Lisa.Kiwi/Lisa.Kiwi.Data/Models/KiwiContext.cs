using System.Data.Entity;

namespace Lisa.Kiwi.Data.Models
{
    class KiwiContext : DbContext
    {
        public KiwiContext()
            : base("KiwiLocalConn")
        {}

        public DbSet<Report> Report { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Remark> Remark { get; set; }
        public DbSet<Contact> Contact { get; set; }
    }
}

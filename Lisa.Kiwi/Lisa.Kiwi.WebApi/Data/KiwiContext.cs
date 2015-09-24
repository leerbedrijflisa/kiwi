using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lisa.Kiwi.WebApi
{
    public class KiwiContext : IdentityDbContext
    {
        public KiwiContext()
            : base("KiwiContext")
        {
        }

        public DbSet<ReportData> Reports { get; set; }
        public DbSet<VehicleData> Vehicles { get; set; }
        public DbSet<LocationData> Locations { get; set; }
        public DbSet<ContactData> Contacts { get; set; }
        public DbSet<FileData> Files { get; set; }
    }
}
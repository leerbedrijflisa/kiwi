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
        public DbSet<StatusChangeData> StatusChanges { get; set; }
        public DbSet<RemarkData> Remarks { get; set; }
        public DbSet<VehicleData> Vehicles { get; set; }
    }
}
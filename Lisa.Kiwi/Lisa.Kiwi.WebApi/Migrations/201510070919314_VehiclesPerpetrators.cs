using System.Data.Entity.Migrations;

namespace Lisa.Kiwi.WebApi
{
    public partial class VehiclesPerpetrators : DbMigration
    {
        public override void Up()
        {
            // drop latitude an longitude fields
            DropColumn("dbo.Locations", "Latitude");
            DropColumn("dbo.Locations", "Longitude");

            // add new foreign key to the perps and vehicles
            AddColumn("dbo.Perpetrators", "ReportData_Id", c => c.Int());
            AddColumn("dbo.Vehicles", "ReportData_Id", c => c.Int());
            CreateIndex("dbo.Perpetrators", "ReportData_Id");
            CreateIndex("dbo.Vehicles", "ReportData_Id");
            AddForeignKey("dbo.Perpetrators", "ReportData_Id", "dbo.Reports", "Id");
            AddForeignKey("dbo.Vehicles", "ReportData_Id", "dbo.Reports", "Id");
            
            // execute migration queries
            var vehiclesQuery = "DECLARE @VehicleReports TABLE(ReportId int, VehicleId int); INSERT INTO @VehicleReports (ReportId, VehicleId) SELECT Id, Vehicle_Id FROM kiwi.dbo.Reports WHERE Vehicle_Id != 0; UPDATE V SET ReportData_Id = VR.ReportId FROM kiwi.dbo.Vehicles V JOIN @VehicleReports VR ON V.Id = VR.VehicleId WHERE VR.VehicleId != 0;";
            var perpetratorsQuery = "DECLARE @PerpetratorReports TABLE(ReportId int, PerpetratorId int); INSERT INTO @PerpetratorReports (ReportId, PerpetratorId) SELECT Id, Perpetrator_Id FROM kiwi.dbo.Reports WHERE Perpetrator_Id != 0; UPDATE P SET ReportData_Id = PR.ReportId FROM kiwi.dbo.Perpetrators P JOIN @PerpetratorReports PR ON P.Id = PR.PerpetratorId WHERE PR.PerpetratorId != 0;";
            
            Sql(vehiclesQuery);
            Sql(perpetratorsQuery);

            // drop foreign keys
            DropForeignKey("dbo.Reports", "Perpetrator_Id", "dbo.Perpetrators");
            DropForeignKey("dbo.Reports", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.Reports", new[] { "Perpetrator_Id" });
            DropIndex("dbo.Reports", new[] { "Vehicle_Id" });
            DropColumn("dbo.Reports", "Perpetrator_Id");
            DropColumn("dbo.Reports", "Vehicle_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "Vehicle_Id", c => c.Int());
            AddColumn("dbo.Reports", "Perpetrator_Id", c => c.Int());
            AddColumn("dbo.Locations", "Longitude", c => c.Single(nullable: false));
            AddColumn("dbo.Locations", "Latitude", c => c.Single(nullable: false));
            DropForeignKey("dbo.Vehicles", "ReportData_Id", "dbo.Reports");
            DropForeignKey("dbo.Perpetrators", "ReportData_Id", "dbo.Reports");
            DropIndex("dbo.Vehicles", new[] { "ReportData_Id" });
            DropIndex("dbo.Perpetrators", new[] { "ReportData_Id" });
            DropColumn("dbo.Vehicles", "ReportData_Id");
            DropColumn("dbo.Perpetrators", "ReportData_Id");
            CreateIndex("dbo.Reports", "Vehicle_Id");
            CreateIndex("dbo.Reports", "Perpetrator_Id");
            AddForeignKey("dbo.Reports", "Vehicle_Id", "dbo.Vehicles", "Id");
            AddForeignKey("dbo.Reports", "Perpetrator_Id", "dbo.Perpetrators", "Id");
        }
    }
}
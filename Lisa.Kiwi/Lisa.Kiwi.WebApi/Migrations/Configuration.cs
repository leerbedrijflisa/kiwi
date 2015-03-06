using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;

namespace Lisa.Kiwi.WebApi.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<KiwiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(KiwiContext context)
        {
            // TODO: add default accounts

            ///* REPORT 1 */
            //ReportData sampleReport = new ReportData
            //{
            //    Description = "Een zakje drugs gevonden.",
            //    Created = DateTime.UtcNow.AddDays(-1),
            //    Location.Building = "Fietsenstalling Azurro",
            //    Guid = Guid.NewGuid().ToString(),
            //    Category = "Drugs"
            //};

            //StatusChangeData sampleStatus = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow.AddDays(-1),
            //    Status = Status.Open.ToString()
            //};

            //VehicleData sampleVehicle = new VehicleData
            //{
            //    Brand = "Mazda",
            //    Color = "Black",
            //    LicensePlate = "ADE 23 SAD",
            //    Report = sampleReport
            //};

            //StatusChangeData sampleStatus2 = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow.AddHours(-20),
            //    Status = Status.Solved.ToString()
            //};

            //RemarkData sampleRemark = new RemarkData
            //{
            //    Created = DateTime.UtcNow.AddHours(-20),
            //    Description = "Er waren meerdere drugs zakjes gevonden door de beveiliging.",
            //    Report = sampleReport
            //};

            //context.Reports.AddOrUpdate(sampleReport);
            //context.StatusChanges.AddOrUpdate(sampleStatus);
            //context.Remarks.AddOrUpdate(sampleRemark);
            //context.StatusChanges.AddOrUpdate(sampleStatus2);
            //context.Vehicles.AddOrUpdate(sampleVehicle);
            ///* EIND REPORT 1 */

            ///* REPORT 2 */
            //sampleReport = new ReportData
            //{
            //    Description = "Mijn mobieltje is gestolen.",
            //    Created = DateTime.UtcNow,
            //    Location = "Lilla",
            //    Time = DateTime.UtcNow.AddHours(-19),
            //    Guid = Guid.NewGuid().ToString(),
            //    UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
            //    Ip = "85.119.106.81",
            //    Type = "Diefstal"
            //};

            //sampleStatus = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow,
            //    Status = Status.Open.ToString()
            //};

            //ContactData sampleContact = new ContactData
            //{
            //    Name = "John Doe",
            //    EmailAddress = "JohnDoe@mydavinci.nl",
            //    PhoneNumber = "0622885793",
            //    Report = sampleReport,
            //    EditToken = Guid.NewGuid()
            //};

            //context.Reports.AddOrUpdate(sampleReport);
            //context.Contacts.AddOrUpdate(sampleContact);
            //context.StatusChanges.AddOrUpdate(sampleStatus);
            ///* EIND REPORT 2 */

            ///* REPORT 3 */
            //sampleReport = new ReportData
            //{
            //    Description = "Auto met gebroken ruit.",
            //    Created = DateTime.UtcNow,
            //    Location = "Achter brandweer parkeerplaats.",
            //    Time = DateTime.UtcNow,
            //    Guid = Guid.NewGuid().ToString(),
            //    UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
            //    Ip = "85.119.106.81",
            //    Type = "Voertuigen"
            //};

            //sampleStatus = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow,
            //    Status = Status.Open.ToString()
            //};


            //context.Reports.AddOrUpdate(sampleReport);
            //context.StatusChanges.AddOrUpdate(sampleStatus);
            ///* EIND REPORT 3 */

            ///* REPORT 4 */
            //sampleReport = new ReportData
            //{
            //    Description = "BRAND Romboutslaan!!!!!",
            //    Created = DateTime.UtcNow,
            //    Location = "ROMBOUTSLAAN",
            //    Time = DateTime.UtcNow,
            //    Guid = Guid.NewGuid().ToString(),
            //    UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
            //    Ip = "85.119.106.81",
            //    Type = "Brand"
            //};

            //sampleStatus = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow,
            //    Status = Status.Open.ToString()
            //};

            //context.Reports.AddOrUpdate(sampleReport);
            //context.StatusChanges.AddOrUpdate(sampleStatus);
            ///* EIND REPORT 4 */

            ///* REPORT 5 */
            //sampleReport = new ReportData
            //{
            //    Description = "Drugs zakjes gevonden",
            //    Created = DateTime.UtcNow.AddHours(-2),
            //    Location = "Azurro",
            //    Time = DateTime.UtcNow.AddHours(-3),
            //    Guid = Guid.NewGuid().ToString(),
            //    UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
            //    Ip = "85.119.106.81",
            //    Type = "Drugs"
            //};

            //sampleStatus = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow.AddHours(-2),
            //    Status = Status.Open.ToString()
            //};

            //sampleRemark = new RemarkData
            //{
            //    Created = DateTime.UtcNow,
            //    Description = "Dit lijkt vaker voor te komen, heeft meer onderzoek nodig.",
            //    Report = sampleReport
            //};

            //sampleStatus2 = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow,
            //    Status = Status.Solved.ToString()
            //};

            //context.Reports.AddOrUpdate(sampleReport);
            //context.StatusChanges.AddOrUpdate(sampleStatus);
            //context.Remarks.AddOrUpdate(sampleRemark);
            //context.StatusChanges.AddOrUpdate(sampleStatus2);
            ///* EIND REPORT 5 */

            ///* REPORT 6 */
            //sampleReport = new ReportData
            //{
            //    Description = "Auto fout geparkeerd. Hij blokeerd de toegang tot de fietsenstalling.",
            //    Created = DateTime.UtcNow,
            //    Location = "Azurro fietsenstalling",
            //    Time = DateTime.UtcNow,
            //    Guid = Guid.NewGuid().ToString(),
            //    UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
            //    Ip = "85.119.106.81",
            //    Type = "Drugs"
            //};

            //sampleStatus = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow,
            //    Status = Status.Open.ToString()
            //};

            //context.Reports.AddOrUpdate(sampleReport);
            //context.StatusChanges.AddOrUpdate(sampleStatus);
            ///* EIND REPORT 6 */

            ///* REPORT 7 */
            //sampleReport = new ReportData
            //{
            //    Description = "Er word veel gescholden en geintimideerd.",
            //    Created = DateTime.UtcNow.AddMinutes(-25),
            //    Location = "Ocra kantine",
            //    Time = DateTime.UtcNow.AddMinutes(-25),
            //    Guid = Guid.NewGuid().ToString(),
            //    UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
            //    Ip = "85.119.106.81",
            //    Type = "Intimidatie"
            //};

            //sampleStatus = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow.AddMinutes(-25),
            //    Status = Status.Open.ToString()
            //};

            //sampleRemark = new RemarkData
            //{
            //    Created = DateTime.UtcNow.AddMinutes(-15),
            //    Description = "Er is met de personen gepraat en en ze zouden verder rustig door gaan met hun pauze.",
            //    Report = sampleReport
            //};

            //sampleStatus2 = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow.AddMinutes(-15),
            //    Status = Status.InProgress.ToString()
            //};

            //context.Reports.AddOrUpdate(sampleReport);
            //context.StatusChanges.AddOrUpdate(sampleStatus);
            //context.Remarks.AddOrUpdate(sampleRemark);
            //context.StatusChanges.AddOrUpdate(sampleStatus2);
            ///* EIND REPORT 7 */

            ///* REPORT 8 */
            //sampleReport = new ReportData
            //{
            //    Description = "Een vechtpartij! 2 jongens en 2 damens zijn aan het vechten en schelden!",
            //    Created = DateTime.UtcNow,
            //    Location = "Ocra kantine",
            //    Time = DateTime.UtcNow,
            //    Guid = Guid.NewGuid().ToString(),
            //    UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
            //    Ip = "85.119.106.81",
            //    Type = "Pesten"
            //};

            //sampleStatus = new StatusChangeData
            //{
            //    Created = DateTime.UtcNow,
            //    Status = Status.Open.ToString()
            //};

            //context.Reports.AddOrUpdate(sampleReport);
            //context.StatusChanges.AddOrUpdate(sampleStatus);
            ///* EIND REPORT 8 */

            //// Set up our accounts
            //CreateRoles(context);
            //CreateUsers(context);

            //base.Seed(context);
        }

        private void CreateRoles(KiwiContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create our roles
            roleManager.Create(new IdentityRole("Administrator"));
        }

        private void CreateUsers(KiwiContext context)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));

            // Goodbye all users
            foreach (var user in userManager.Users.ToList())
            {
                userManager.Delete(user);
            }

            // Add a test administrator account (name=admin pass=toor42)
            var admin = new IdentityUser("admin");
            ThrowIfFailed(userManager.Create(admin, "toor42"));
            userManager.AddToRole(admin.Id, "Administrator");

            // Add a test "beveiliger" account (name=beveiliger pass=hello)
            var dashboardUser = new IdentityUser("beveiliger");
            userManager.Create(dashboardUser, "helloo");

            // Add a test "beveiliger2" account (name=beveiliger2 pass=hello2)
            var dashboardUser2 = new IdentityUser("beveiliger2");
            userManager.Create(dashboardUser2, "helloo2");

            // Add a test "hoofdbeveiliger" account (name=hoofdbeveiliger pass=masterpass)
            var headOfSecurity = new IdentityUser("Hoofdbeveiliging");
            ThrowIfFailed(userManager.Create(headOfSecurity, "masterpass"));
            userManager.AddToRole(headOfSecurity.Id, "Administrator");
        }

        private void ThrowIfFailed(IdentityResult result)
        {
            if (result.Succeeded)
                return;

            var msg = result.Errors.Aggregate(
                "Failed to create user!",
                (current, error) => current + ("\n" + error));
            throw new Exception(msg);
        }
    }
}

using System;
using System.Collections.Generic;
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
            /* REPORT 1 */

            ReportData sampleReport = new ReportData
            {
                Description = "Een zakje drugs gevonden.",
                Created = DateTime.UtcNow.AddDays(-1),
                Location = new LocationData
                {
                    Building = "Azurro",
                    Description = "Fietsenstalling",
                    Latitude = 0f,
                    Longitude = 0f
                },
                Guid = Guid.NewGuid().ToString(),
                Category = "Drugs",
                Vehicle = new VehicleData
                {
                    Brand = "Mazda",
                    Color = "Black",
                    LicensePlate = "ADE 23 SAD"
                },
                StatusChanges = new List<StatusChangeData>
                {
                    new StatusChangeData
                    {
                        Created = DateTime.UtcNow.AddDays(-1),
                        Status = Status.Open.ToString()
                    },
                    new StatusChangeData
                    {
                        Created = DateTime.UtcNow.AddHours(-20),
                        Status = Status.Solved.ToString()
                    }
                }
            };

            RemarkData sampleRemark = new RemarkData
            {
                Created = DateTime.UtcNow.AddHours(-20),
                Description = "Er waren meerdere drugs zakjes gevonden door de beveiliging.",
                Report = sampleReport
            };

            context.Reports.AddOrUpdate(sampleReport);
            context.Remarks.AddOrUpdate(sampleRemark);
            /* EIND REPORT 1 */

            /* REPORT 2 */
            sampleReport = new ReportData
            {
                Description = "Mijn mobieltje is gestolen.",
                Created = DateTime.UtcNow,
                Location = new LocationData
                {
                    Building = "Lilla",
                    Description = "Plaza",
                    Latitude = 0f,
                    Longitude = 0f
                },
                Guid = Guid.NewGuid().ToString(),
                Category = "Theft",
                Contact = new ContactData
                {
                    Name = "John Doe",
                    EmailAddress = "JohnDoe@mydavinci.nl",
                    PhoneNumber = "0622885793"
                },
                StatusChanges = new List<StatusChangeData>
                {
                    new StatusChangeData
                    {
                        Created = DateTime.UtcNow,
                        Status = Status.Open.ToString()
                    }
                }
            };

            context.Reports.AddOrUpdate(sampleReport);
            /* EIND REPORT 2 */

            /* REPORT 3 */
            sampleReport = new ReportData
            {
                Description = "Auto met gebroken ruit.",
                Created = DateTime.UtcNow,
                Location = new LocationData
                {
                    Building = "Brandweer",
                    Description = "Parkeerplaats",
                    Latitude = 0f,
                    Longitude = 0f
                },
                Guid = Guid.NewGuid().ToString(),
                Category = "Other",
                StatusChanges = new List<StatusChangeData>
                {
                    new StatusChangeData
                    {
                        Created = DateTime.UtcNow,
                        Status = Status.Open.ToString()
                    }
                }
            };
            context.Reports.AddOrUpdate(sampleReport);
            /* EIND REPORT 3 */

            /* REPORT 5 */
            sampleReport = new ReportData
            {
                Description = "Drugs zakjes gevonden",
                Created = DateTime.UtcNow.AddHours(-2),
                Location = new LocationData
                {
                    Building = "Azzurro",
                    Description = "Fietsenstalling",
                    Latitude = 0f,
                    Longitude = 0f
                },
                Guid = Guid.NewGuid().ToString(),
                Category = "Drugs",
                StatusChanges = new List<StatusChangeData>
                {
                    new StatusChangeData
                    {
                        Created = DateTime.UtcNow.AddHours(-2),
                        Status = Status.Open.ToString()
                    },
                    new StatusChangeData
                    {
                        Created = DateTime.UtcNow,
                        Status = Status.Solved.ToString()
                    }
                }
            };

            sampleRemark = new RemarkData
            {
                Created = DateTime.UtcNow,
                Description = "Dit lijkt vaker voor te komen, heeft meer onderzoek nodig.",
                Report = sampleReport
            };

            context.Reports.AddOrUpdate(sampleReport);
            context.Remarks.AddOrUpdate(sampleRemark);
            /* EIND REPORT 5 */

            /* REPORT 6 */
            sampleReport = new ReportData
            {
                Description = "Auto fout geparkeerd. Hij blokeert de toegang tot de fietsenstalling.",
                Created = DateTime.UtcNow,
                Guid = Guid.NewGuid().ToString(),
                Category = "Other",
                Location = new LocationData
                {
                    Building = "Azurro",
                    Description = "Fietsenstalling",
                    Latitude = 0f,
                    Longitude = 0f
                },
                StatusChanges = new List<StatusChangeData>
                {
                    new StatusChangeData
                    {
                        Created = DateTime.UtcNow,
                        Status = Status.Open.ToString()
                    }
                }
            };

            context.Reports.AddOrUpdate(sampleReport);
            /* EIND REPORT 6 */

            /* REPORT 7 */
            sampleReport = new ReportData
            {
                Description = "Er word veel gescholden en geintimideerd.",
                Created = DateTime.UtcNow.AddMinutes(-25),
                Guid = Guid.NewGuid().ToString(),
                Category = "Fight",
                Location = new LocationData
                {
                    Building = "Ocra",
                    Description = "Kantine",
                    Latitude = 0f,
                    Longitude = 0f
                },
                StatusChanges = new List<StatusChangeData>
                {
                    new StatusChangeData
                    {
                        Created = DateTime.UtcNow.AddMinutes(-25),
                        Status = Status.Open.ToString()
                    },
                    new StatusChangeData
                    {
                        Created = DateTime.UtcNow.AddMinutes(-15),
                        Status = Status.InProgress.ToString()
                    }
                }
            };

            sampleRemark = new RemarkData
            {
                Created = DateTime.UtcNow.AddMinutes(-15),
                Description = "Er is met de personen gepraat en en ze zouden verder rustig door gaan met hun pauze.",
                Report = sampleReport
            };

            context.Reports.AddOrUpdate(sampleReport);
            context.Remarks.AddOrUpdate(sampleRemark);
            /* EIND REPORT 7 */

            /* REPORT 8 */
            sampleReport = new ReportData
            {
                Description = "Een vechtpartij! 2 jongens en 2 damens zijn aan het vechten en schelden!",
                Created = DateTime.UtcNow,
                Location = new LocationData
                {
                    Building = "Ocra",
                    Description = "Kantine",
                    Latitude = 100f,
                    Longitude = 100f
                },
                Guid = Guid.NewGuid().ToString(),
                Category = "Bullying",
                StatusChanges = new List<StatusChangeData>
                {
                    new StatusChangeData
                    {
                        Created = DateTime.UtcNow,
                        Status = Status.Open.ToString()
                    }
                }
            };

            context.Reports.AddOrUpdate(sampleReport);
            /* EIND REPORT 8 */

            // Set up our accounts
            CreateRoles(context);
            CreateUsers(context);

            base.Seed(context);
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
            var headOfSecurity = new IdentityUser("HBD");
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

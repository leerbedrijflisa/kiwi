using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using System.Transactions;

namespace Lisa.Kiwi.WebApi
{
    internal sealed class Configuration : DbMigrationsConfiguration<KiwiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(KiwiContext context)
        {
#if !DEBUG
            //Truncates(context);
#endif
            //// Set up our accounts
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


        private int Truncates(DbContext db, params string[] tables)
        {
            var target = new List<string>();
            var result = 0;

            if (tables == null || tables.Length == 0)
            {
                target = GetTableList(db);
            }
            else
            {
                target.AddRange(tables);
            }

            using (var scope = new TransactionScope())
            {
                foreach (var table in target)
                {
                    result += db.Database.ExecuteSqlCommand(string.Format("DELETE FROM  [{0}]", table));
                    db.Database.ExecuteSqlCommand(string.Format("DBCC CHECKIDENT ([{0}], RESEED, 0)", table));
                }

                scope.Complete();
            }

            return result;
        }

        private List<string> GetTableList(DbContext db)
        {
            return db.GetType().GetProperties()
                .Where(x => x.PropertyType.Name == "DbSet`1")
                .Select(x => x.Name).ToList();
        }
    }

}


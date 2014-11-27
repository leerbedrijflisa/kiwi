using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lisa.Kiwi.Data.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<KiwiContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(KiwiContext context)
		{
			Report sampleReport = new Report
			{
				Description = "Dit is een test report. Lorem Lorem Kaasaus.",
				Created = DateTime.UtcNow,
				Location = "Het Lisa lokaal",
				Time = DateTime.UtcNow.AddDays(-1),
				Guid = Guid.NewGuid().ToString(),
				UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
				Ip = "85.119.106.81",
				Type = ReportType.Digital,
				Hidden = false
			};

			Status sampleStatus = new Status
			{
				Created = DateTime.UtcNow,
				Name = StatusName.InProgress,
				Report = sampleReport
			};

			Status sampleStatus2 = new Status
			{
				Created = DateTime.UtcNow,
				Name = StatusName.InProgress,
				Report = sampleReport
			};

			Remark sampleRemark = new Remark
			{
				Created = DateTime.UtcNow,
				Description = "Laanaamaakai. Cheese is da bawm.",
				Report = sampleReport
			};

			context.Reports.AddOrUpdate(sampleReport);
			context.Statuses.AddOrUpdate(sampleStatus);
			context.Statuses.AddOrUpdate(sampleStatus2);
			context.Remarks.AddOrUpdate(sampleRemark);

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
			
			// Add a test "hoofdbeveiliger" account (name=hoofdbeveiliger pass=masterpass)
            var headOfSecurity = new IdentityUser("hoofdbeveiliger");
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
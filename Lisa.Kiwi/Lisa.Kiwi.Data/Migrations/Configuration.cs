using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

			var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			// Create our Administrator role
			roleManager.Create(new IdentityRole("Administrator"));
			
			// Add a test administrator account (name=admin pass=toor42)
			var user = new IdentityUser("admin");
			var adminresult = userManager.Create(user, "toor42");

			// Add User Admin to Role Admin
			if (adminresult.Succeeded)
			{
				var result = userManager.AddToRole(user.Id, "Administrator");
			}

			base.Seed(context);
		}
	}
}
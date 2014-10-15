using System;
using System.Collections.Generic;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.Data.Migrations
{
    using System.Data.Entity.Migrations;

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
                Created = DateTime.Now,
                Location = "Het Lisa lokaal",
                Time = DateTime.Now.AddDays(-1),
                Guid = Guid.NewGuid().ToString(),
                UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
                Ip = "85.119.106.81",
                Type = ReportType.Digital,
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        EmailAddress = "jordischarloo@gmail.com",
                        PhoneNumber = "+31642904875",
                        StudentNumber = 99016300
                    }
                }
            };

            ReportSettings sampleReportSettings = new ReportSettings
            {
                Visible = true,
                Report = sampleReport
            };

            Status sampleStatus = new Status
            {
                Created = DateTime.Now,
                Name = StatusName.InProgress,
                Report = sampleReport
            };

            Status sampleStatus2 = new Status
            {
                Created = DateTime.Now,
                Name = StatusName.InProgress,
                Report = sampleReport
            };

            Remark sampleRemark = new Remark
            {
                Created = DateTime.Now,
                Description = "Laanaamaakai. Cheese is da bawm.",
                Report = sampleReport
            };

            context.ReportSettings.AddOrUpdate(sampleReportSettings);
            context.Reports.AddOrUpdate(sampleReport);
            context.Statuses.AddOrUpdate(sampleStatus);
            context.Statuses.AddOrUpdate(sampleStatus2);
            context.Remarks.AddOrUpdate(sampleRemark);
        }
    }
}

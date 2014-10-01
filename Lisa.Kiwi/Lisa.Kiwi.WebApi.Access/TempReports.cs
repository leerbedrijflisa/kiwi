using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lisa.Kiwi.WebApi.Access;

namespace Lisa.Kiwi.WebApi.Access
{
    public class TempReports
    {
        public TempReports()
        {
            List.Add(new Report
            {
                Description = "Dit is een test report. Lorem Lorem Kaasaus.",
                Created = DateTime.Now.AddHours(4),
                Location = "Het Lisa lokaal",
                Time = DateTime.Now.AddDays(-1),
                Guid = "a34b27bd-ff29-4859-9680-37af353d5e0f",
                UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
                Ip = "85.119.106.81",
                Priority = ReportPriority.High,
                Type = ReportType.Drugs,
                Status = new Status
                {
                    Name = StatusName.Open,
                    Created = DateTime.Now
                },
                Contacts = new List<Contact>()
                {
                    new Contact {
                        Name = "Frank van Driel",
                        EmailAddress = "Frank.vandriel@hotmail.com",
                    }
                }
            });

            List.Add(new Report
            {
                Description = "HELP!!! een lage prioritijd. ",
                Created = DateTime.Now.AddSeconds(34),
                Location = "Het Lisa lokaal",
                Time = DateTime.Now.AddDays(-1),
                Guid = "a15d143c-d30f-4e73-b05a-b3c1c1b7944b",
                UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
                Ip = "85.119.106.81",
                Priority = ReportPriority.Low,
                Type = ReportType.Burglary,
                Status = new Status
                {
                    Name = StatusName.Open,
                    Created = DateTime.Now
                },
                Contacts = new List<Contact>()
                {
                    new Contact {
                        Name = "Frank van Driel",
                        EmailAddress = "Frank.vandriel@hotmail.com",
                    }
                }
            });

            List.Add(new Report
            {
                Description = "Dit is een andere titel om het makkelijk te zien.",
                Created = DateTime.Now.AddDays(100),
                Location = "Het Lisa lokaal",
                Time = DateTime.Now.AddDays(-1),
                Guid = "00c13ba1-83b5-4aeb-9b0c-b87af8341a4e",
                UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
                Ip = "85.119.106.81",
                Priority = ReportPriority.Normal,
                Type = ReportType.Fire,
                Status = new Status
                {
                    Name = StatusName.Open,
                    Created = DateTime.Now
                }
            });

            List.Add(new Report
            {
                Description = "Euhmm wut 0_o ??",
                Created = DateTime.Now,
                Location = "Het Lisa lokaal",
                Time = DateTime.Now.AddDays(-1),
                Guid = "0ef24dc5-5980-4fa0-b1ad-fe46a1391808",
                UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
                Ip = "85.119.106.81",
                Priority = ReportPriority.Normal,
                Type = ReportType.Theft,
                Status = new Status
                {
                    Name = StatusName.Solved,
                    Created = DateTime.Now
                },
                Contacts = new List<Contact>()
                {
                    new Contact {
                        Name = "Frank van Driel",
                        EmailAddress = "Frank.vandriel@hotmail.com",
                    }
                }
            });
        }

        private IList<Report> List = new List<Report>();

        public IEnumerable<Report> GetAll()
        {
            return List;
        }

        public Report GetReport(string guid)
        {
            foreach (var report in List)
            {
                if (report.Guid == guid)
                    return report;
            }
            return null;
        }
    }
}

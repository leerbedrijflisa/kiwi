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
        public IEnumerable<Reports> GetAll()
        {
            IList<Reports> list = new List<Reports>();
            list.Add(new Reports
            {
                Description = "Dit is een test report. Lorem Lorem Kaasaus.",
                Created = DateTime.Now.AddHours(4),
                Location = "Het Lisa lokaal",
                Time = DateTime.Now.AddDays(-1),
                Guid = Guid.NewGuid().ToString(),
                UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
                Ip = "85.119.106.81",
                Priority = ReportPriority.High,
                Type = ReportType.Drugs,
                Status = new Status
                {
                   Name = StatusName.Open,
                   Created = DateTime.Now
                }
            });

            list.Add(new Reports
            {
                Description = "HELP!!! een lage prioritijd. ",
                Created = DateTime.Now.AddSeconds(34),
                Location = "Het Lisa lokaal",
                Time = DateTime.Now.AddDays(-1),
                Guid = Guid.NewGuid().ToString(),
                UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
                Ip = "85.119.106.81",
                Priority = ReportPriority.Low,
                Type = ReportType.Burglary,
                Status = new Status
                {
                   Name = StatusName.Open,
                   Created = DateTime.Now
                }
            });

            list.Add(new Reports
            {
                Description = "Dit is een andere titel om het makkelijk te zien.",
                Created = DateTime.Now.AddDays(100),
                Location = "Het Lisa lokaal",
                Time = DateTime.Now.AddDays(-1),
                Guid = Guid.NewGuid().ToString(),
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

            list.Add(new Reports
            {
                Description = "Euhmm wut 0_o ??",
                Created = DateTime.Now,
                Location = "Het Lisa lokaal",
                Time = DateTime.Now.AddDays(-1),
                Guid = Guid.NewGuid().ToString(),
                UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:31.0) Gecko/20100101 Firefox/31.0",
                Ip = "85.119.106.81",
                Priority = ReportPriority.Normal,
                Type = ReportType.Theft,
                Status = new Status
                {
                   Name = StatusName.Solved,
                   Created = DateTime.Now
                }
            });

            return list;
        }
    }
}

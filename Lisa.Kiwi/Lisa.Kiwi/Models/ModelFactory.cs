using System;
using System.Linq;

namespace Lisa.Kiwi.WebApi
{
    internal class ModelFactory
    {
        public Report Create(ReportData reportData)
        {
            var report = new Report
            {
                Id = reportData.Id,
                Description = reportData.Description,
                Created = reportData.Created,
                Location = reportData.Location,
                Category = reportData.Type,
                IsVisible = true,
                CurrentStatus = Status.Open
            };

            // Add information about the most recent status change only, because
            // that's the current status of the report.
            var statusChangeData = reportData.StatusChanges
                .OrderByDescending(s => s.Created)
                .FirstOrDefault();

            if (statusChangeData != null)
            {
                report.IsVisible = statusChangeData.IsVisible;
                report.CurrentStatus = ToStatus(statusChangeData.Status);
            }

            return report;
        }

        private Status ToStatus(string status)
        {
            switch (status.ToLower())
            {
                case "open": return Status.Open;
                case "solved": return Status.Solved;
                case "inprogress": return Status.InProgress;
                case "transferred": return Status.Transferred;
                default: throw new ArgumentException();
            }
        }
    }
}
using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi
{
    internal class DataFactory
    {
        public ReportData Create(Report report)
        {
            return new ReportData
            {
                Description = report.Description,
                Location = report.Location,
                Type = report.Category
            };
        }

        public void Modify(ReportData reportData, JToken json)
        {
            var currentStatus = reportData.StatusChanges
                .OrderByDescending(s => s.Created)
                .FirstOrDefault();

            var statusChangeData = new StatusChangeData
            {
                Created = DateTimeOffset.Now,
                IsVisible = json.Value<bool?>("isVisible") ?? currentStatus.IsVisible,
                Status = json.Value<string>("currentStatus") ?? currentStatus.Status
            };

            reportData.StatusChanges.Add(statusChangeData);
        }

        public RemarkData Create(Remark remark)
        {
            var remarkData = new RemarkData
            {
                Id = remark.Id,
                Created = remark.Created,
                Description = remark.Description
            };

            return remarkData;
        }

        public void Modify(RemarkData remarkData, JToken json)
        {
            remarkData.Description = json.Value<string>("description");
        }
    }
}
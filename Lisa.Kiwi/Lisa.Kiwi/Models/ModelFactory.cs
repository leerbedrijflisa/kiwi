using System;
using System.Linq;
using Lisa.Kiwi.WebApi.Models;

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

        public Remark Create(RemarkData remarkData)
        {
            var remark = new Remark
            {
                Id = remarkData.Id,
                Created = remarkData.Created,
                Description = remarkData.Description,
                Report = remarkData.Report.Id
            };

            return remark;
        }

        public Vehicle Create(VehicleData vehicleData)
        {
            var vehicle = new Vehicle
            {
                Id = vehicleData.Id,
                Brand = vehicleData.Brand,
                Color = vehicleData.Color,
                LicensePlate = vehicleData.LicensePlate,
                Model = vehicleData.Model,
                Report = vehicleData.Report.Id
            };

            return vehicle;
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
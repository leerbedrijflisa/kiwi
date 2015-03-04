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
                Category = reportData.Category,
                IsVisible = true,
                CurrentStatus = Status.Open,
                Contact = reportData.Contact != null ? Create(reportData.Contact) : null
            };

            if (reportData.Location != null)
            {
                report.Location = new Location
                {
                    Building = reportData.Location.Building,
                    Description = reportData.Location.Description
                };
                
            }
         


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
                Brand = vehicleData.Brand,
                Color = vehicleData.Color,
                LicensePlate = vehicleData.LicensePlate,
                Model = vehicleData.Model
            };

            return vehicle;
        }

        public Contact Create(ContactData contactData)
        {
            var contact = new Contact
            {
                EmailAddress = contactData.EmailAddress,
                EditToken = contactData.EditToken,
                Name = contactData.Name,
                PhoneNumber = contactData.PhoneNumber,
                Report = contactData.Report.Id
            };

            return contact;
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
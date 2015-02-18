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
                Building = report.Building,
                Location = report.Location,
                Type = report.Category
            };
        }

        public void Modify(ReportData reportData, JToken json)
        {
            reportData.Type = json.Value<string>("category") ?? reportData.Type;
            reportData.Building = json.Value<string>("building") ?? reportData.Building;
            reportData.Location = json.Value<string>("location") ?? reportData.Location;
            reportData.Description = json.Value<string>("description") ?? reportData.Description;

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

        public VehicleData Create(Vehicle vehicle)
        {
            var vehicleData = new VehicleData
            {
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                Color = vehicle.Color,
                LicensePlate = vehicle.LicensePlate,
                Model = vehicle.Model
            };

            return vehicleData;
        }

        public ContactData Create(Contact contact)
        {
            var contactData = new ContactData
            {
                Id = contact.Id,
                EditToken = contact.EditToken,
                EmailAddress = contact.EmailAddress,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber
            };

            return contactData;
        }
    }
}
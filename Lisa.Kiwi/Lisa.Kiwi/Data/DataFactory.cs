using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi
{
    internal class DataFactory
    {
        public ReportData Create(Report report)
        {
            var location = new LocationData
            {
                Building = report.Location.Building,
                Description = report.Location.Description
            };

            return new ReportData
            {
                Description = report.Description,
                Location = location,
                Category = report.Category
            };
        }

        public void Modify(ReportData reportData, JToken json)
        {
            reportData.Category = json.Value<string>("category") ?? reportData.Category;
            
            if (json["location"] != null)
            {
                reportData.Location = Modify(reportData.Location, json["location"]);
            }

            reportData.Description = json.Value<string>("description") ?? reportData.Description;
            reportData.IsUnconscious = json.Value<bool?>("isUnconscious") ?? reportData.IsUnconscious;

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
                EditToken = contact.EditToken,
                EmailAddress = contact.EmailAddress,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber
            };

            return contactData;
        }

        public LocationData Modify(LocationData locationData, JToken json)
        {
            var data = locationData ?? new LocationData();
            data.Building = json["building"] != null ? json.Value<string>("building") : data.Building;
            data.Description = json["description"] != null ? json.Value<string>("description") : data.Description;
            return data;
        }
    }
}
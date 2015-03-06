using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi
{
    internal class DataFactory
    {
        public ReportData Create(Report report)
        {
            //var location = report.Location != null ? new LocationData
            //{
            //    Building = report.Location.Building,
            //    Description = report.Location.Description
            //} : new LocationData();

            //var contact = report.Contact != null ? new ContactData
            //{
            //    EmailAddress = report.Contact.EmailAddress,
            //    Name = report.Contact.Name,
            //    PhoneNumber = report.Contact.PhoneNumber
            //} : new ContactData();

            var locationData = new LocationData();
            var contactData = new ContactData();

            return new ReportData
            {
                Description = report.Description,
                Location = locationData,
                Contact = contactData,
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
            if (json["contact"] != null)
            {
                reportData.Contact = Modify(reportData.Contact, json["contact"]);
            }

            reportData.Description = json.Value<string>("description") ?? reportData.Description;

            reportData.IsUnconscious = json.Value<bool?>("isUnconscious") ?? reportData.IsUnconscious;

            reportData.FighterCount = json.Value<int?>("fighterCount") ?? reportData.FighterCount;
            reportData.IsWeaponPresent = json.Value<bool?>("isWeaponPresent") ?? reportData.IsWeaponPresent;

            reportData.StolenObject = json.Value<string>("stolenObject") ?? reportData.StolenObject;
            reportData.DateOfTheft = json.Value<DateTime?>("dateOfTheft") ?? reportData.DateOfTheft;

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

        public ContactData Modify(ContactData contactData, JToken json)
        {
            var data = contactData ?? new ContactData();
            data.Name = json["name"] != null ? json.Value<string>("name") : data.Name;
            data.PhoneNumber = json["phoneNumber"] != null ? json.Value<string>("phoneNumber") : data.PhoneNumber;
            data.EmailAddress = json["emailAddress"] != null ? json.Value<string>("emailAddress") : data.EmailAddress;
            return data;
        }
    }
}
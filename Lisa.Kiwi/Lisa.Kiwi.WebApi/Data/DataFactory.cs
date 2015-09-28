using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer.Utilities;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi
{
    internal class DataFactory
    {
        public ReportData Create(JToken json)
        {
            var reportData = new ReportData();
            Modify(reportData, json);
            return reportData;
        }

        public void Modify(ReportData reportData, JToken json)
        {
            reportData.Category = json.Value<string>("category") ?? reportData.Category;

            reportData.IsVisible = json.Value<bool?>("isVisible") ?? reportData.IsVisible;
            
            var statusString = json["status"] != null ? json.Value<string>("status") : null;
            reportData.Status = statusString != null ? (StatusEnum)Enum.Parse(typeof(StatusEnum), statusString, true) : reportData.Status;

            reportData.Description = json.Value<string>("description") ?? reportData.Description;

            reportData.IsUnconscious = json.Value<bool?>("isUnconscious") ?? reportData.IsUnconscious;

            reportData.StolenObject = json.Value<string>("stolenObject") ?? reportData.StolenObject;
            reportData.DateOfTheft = json.Value<DateTime?>("dateOfTheft") ?? reportData.DateOfTheft;

            reportData.DrugsAction = json.Value<string>("drugsAction") ?? reportData.DrugsAction;
            
            reportData.FighterCount = json.Value<int?>("fighterCount") ?? reportData.FighterCount;
            reportData.IsWeaponPresent = json.Value<bool?>("isWeaponPresent") ?? reportData.IsWeaponPresent;

            reportData.WeaponType = json.Value<string>("weaponType") ?? reportData.WeaponType;
            reportData.WeaponLocation = json.Value<string>("weaponLocation") ?? reportData.WeaponLocation;

            reportData.Victim = json.Value<string>("victim") ?? reportData.Victim;
            reportData.VictimName = json.Value<string>("victimName") ?? reportData.VictimName;


            if (json["location"] != null)
            {
                reportData.Location = Modify(reportData.Location, json["location"]);
            }
            if (json["contact"] != null)
            {
                reportData.Contact = Modify(reportData.Contact, json["contact"]);
            }
            if (json["perpetrators"] != null)
            {
                reportData.Perpetrators = ModifyPerpetrators(json["perpetrators"]);
            }
            if (json["vehicles"] != null)
            {
                reportData.Vehicles = ModifyVehicles(json["vehicles"]);
            }
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

        private LocationData Modify(LocationData locationData, JToken json)
        {
            var data = locationData ?? new LocationData();
            data.Building = json["building"] != null ? json.Value<string>("building") : data.Building;
            data.Description = json["description"] != null ? json.Value<string>("description") : data.Description;
            return data;
        }

        private ContactData Modify(ContactData contactData, JToken json)
        {
            var data = contactData ?? new ContactData();
            data.Name = json["name"] != null ? json.Value<string>("name") : data.Name;
            data.PhoneNumber = json["phoneNumber"] != null ? json.Value<string>("phoneNumber") : data.PhoneNumber;
            data.EmailAddress = json["emailAddress"] != null ? json.Value<string>("emailAddress") : data.EmailAddress;
            return data;
        }

        private ICollection<PerpetratorData> ModifyPerpetrators(JToken json)
        {
            var perpetrators = new List<PerpetratorData>();

            foreach (var perpetratorJson in json)
            {
                var perpetrator = new PerpetratorData
                {
                    Name = perpetratorJson.Value<string>("name"),
                    Clothing = perpetratorJson.Value<string>("clothing"),
                    MinimumAge = perpetratorJson.Value<int>("minimumAge"),
                    MaximumAge = perpetratorJson.Value<int>("maximumAge"),
                    UniqueProperties = perpetratorJson.Value<string>("uniqueProperties")
                };

                var textInfo = new CultureInfo("en-US", false).TextInfo;

                var sex = perpetratorJson["sex"] != null ? textInfo.ToTitleCase(perpetratorJson.Value<string>("sex")) : null;
                var skinColor = perpetratorJson["skinColor"] != null ? textInfo.ToTitleCase(perpetratorJson.Value<string>("skinColor")) : null;

                perpetrator.SkinColor = skinColor != null ? (SkinColorEnum) Enum.Parse(typeof (SkinColorEnum), skinColor, true) : SkinColorEnum.Unknown;
                perpetrator.Sex = sex != null ? (SexEnum) Enum.Parse(typeof (SexEnum), sex, true) : SexEnum.Unknown;

                perpetrators.Add(perpetrator);
            }

            return perpetrators;
        } 

        private ICollection<VehicleData> ModifyVehicles(JToken json)
        {
            var vehicles = new List<VehicleData>();

            foreach (var vehicleJson in json)
            {
                var vehicle = new VehicleData
                {
                    Brand = vehicleJson.Value<string>("brand"),
                    AdditionalFeatures = vehicleJson.Value<string>("additionalFeatures"),
                    Color = vehicleJson.Value<string>("color"),
                    NumberPlate = vehicleJson.Value<string>("numberPlate")
                };
                
                var vehicleType = new CultureInfo("en-US", false).TextInfo.ToTitleCase(vehicleJson.Value<string>("vehicleType") ?? "Other");

                vehicle.VehicleType = (VehicleTypeEnum) Enum.Parse(typeof (VehicleTypeEnum), vehicleType);

                vehicles.Add(vehicle);
            }

            return vehicles;
        }
    }
}
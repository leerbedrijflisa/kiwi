using Lisa.Kiwi.WebApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.Web
{
    public class ModelFactory
    {
        public Report Create(CategoryViewModel viewModel)
        {
            return new Report
            {
                Category = viewModel.Category
            };
        }

        public void Modify(Report report, LocationViewModel viewModel)
        {
            if (report.Location == null)
            {
                report.Location = new Location();
            }
            report.Location.Building = viewModel.Building;
        }

        public void Modify(Report report, AdditionalLocationViewModel viewModel)
        {
            if (report.Location == null)
            {
                report.Location = new Location();
            }
            report.Location.Description = viewModel.AdditionalLocation;
        }

        public void Modify(Report report, FirstAidViewModel viewModel)
        {
            report.IsUnconscious = viewModel.IsUnconscious == "Ja";
        }

        public void Modify(Report report, TheftViewModel viewModel)
        {
            report.StolenObject = viewModel.StolenObject;
            report.DateOfTheft = viewModel.DateOfTheft;
            report.Description = viewModel.Description;
            report.Vehicles = GetVehicles(viewModel.Vehicles);
            report.Perpetrators = GetPerpetrators(viewModel.Perpetrators);
        }

        public void Modify(Report report, DrugsViewModel viewModel)
        {
            report.DrugsAction = viewModel.Action;
            report.Description = viewModel.Description;
            report.Vehicles = GetVehicles(viewModel.Vehicles);
            report.Perpetrators = GetPerpetrators(viewModel.Perpetrators);
        }

        public void Modify(Report report, FightViewModel viewModel)
        {
            report.FighterCount = viewModel.FighterCount;
            report.IsWeaponPresent = viewModel.IsWeaponPresent;
            report.Description = viewModel.Description;
        }

        public void Modify(Report report, WeaponViewModel viewModel)
        {
            report.WeaponType = viewModel.Type;
            if (viewModel.Type == "Anders")
            {
                report.WeaponType = viewModel.OtherType;
            }
            report.WeaponLocation = viewModel.Location;
            report.Description = viewModel.Description;
        }

        public void Modify(Report report, NuisanceViewModel viewModel)
        {
            report.Description = viewModel.Description;
            report.Vehicles = GetVehicles(viewModel.Vehicles);
            report.Perpetrators = GetPerpetrators(viewModel.Perpetrators);
        }

        public void Modify(Report report, BullyingViewModel viewModel)
        {
            report.Description = viewModel.Description;
            report.VictimName = viewModel.VictimName;
            report.Perpetrators = GetPerpetrators(viewModel.Perpetrators);
        }

        public void Modify(Report report, OtherViewModel viewModel)
        {
            report.Description = viewModel.Description;
            report.Vehicles = GetVehicles(viewModel.Vehicles);
            report.Perpetrators = GetPerpetrators(viewModel.Perpetrators);
        }
        
        public void Modify(Report report, ContactViewModel viewModel)
        {
             if(report.Contact == null)
             {
                 report.Contact = new Contact();
             }
             report.Contact.Name = viewModel.Name;
             report.Contact.PhoneNumber = viewModel.PhoneNumber;
             report.Contact.EmailAddress = viewModel.EmailAddress;
        }

        public void Modify(Report report, ContactRequiredViewModel viewModel)
        {
            if (report.Contact == null)
            {
                report.Contact = new Contact();
            }
            report.Contact.Name = viewModel.Name;
            report.Contact.PhoneNumber = viewModel.PhoneNumber;
            report.Contact.EmailAddress = viewModel.EmailAddress;
        }

        public void Modify(Report report, StatusChangeViewModel viewModel)
        {
            report.Status = viewModel.Status;
            report.IsVisible = viewModel.IsVisible;
        }

        public void Modify(Report report, DoneViewModel viewModel)
        {
            report.Category = viewModel.Category;
            report.Description = viewModel.Description;
            report.DrugsAction = viewModel.DrugsAction;
            report.FighterCount = viewModel.FighterCount;
            report.IsUnconscious = viewModel.IsUnconscious;
            report.DateOfTheft = viewModel.DateOfTheft;
            report.StolenObject = viewModel.StolenObject;
            report.Victim = viewModel.Victim;
            report.VictimName = viewModel.VictimName;
            report.IsWeaponPresent = viewModel.IsWeaponPresent;
            report.WeaponLocation = viewModel.WeaponLocation;
            report.WeaponType = viewModel.WeaponType;
            report.WeaponType = viewModel.WeaponType;
            report.Location = viewModel.Location;
            report.Perpetrators = viewModel.Perpetrators;
            report.Vehicles = viewModel.Vehicles;

            if (viewModel.ContactName != null || viewModel.ContactPhoneNumber != null || viewModel.ContactEmail != null)
            {
                if (report.Contact == null)
                {
                    report.Contact = new Contact();
                }
                report.Contact.EmailAddress = viewModel.ContactEmail;
                report.Contact.PhoneNumber = viewModel.ContactPhoneNumber;
                report.Contact.Name = viewModel.ContactName;
            }
            else
            {
                report.Contact = null;
            }
        }

        private IEnumerable<Perpetrator> GetPerpetrators(string json)
        {
            var perpetratorsJson = json != null ? JsonConvert.DeserializeObject<JToken>(json) : new JArray();
            var perpetrators = new List<Perpetrator>();

            foreach (var perpetratorJson in perpetratorsJson)
            {
                var perpetrator = new Perpetrator
                {
                    Name = perpetratorJson.Value<string>("Name"),
                    Clothing = perpetratorJson.Value<string>("Clothing"),
                    UniqueProperties = perpetratorJson.Value<string>("UniqueProperties")
                };

                var skinColor = perpetratorJson["SkinColor"] != null ? perpetratorJson.Value<string>("SkinColor") : null;
                perpetrator.SkinColor = skinColor != null ? (SkinColorEnum) Enum.Parse(typeof (SkinColorEnum), skinColor, true) : SkinColorEnum.Unknown;

                var sex = perpetratorJson["Sex"] != null ? perpetratorJson.Value<string>("Sex") : null;
                perpetrator.Sex = sex != null ? (SexEnum) Enum.Parse(typeof (SexEnum), sex, true) : SexEnum.Unknown;

                var ages = perpetratorJson.Value<string>("AgeRange").Split('-');

                perpetrator.MinimumAge = int.Parse(ages[0]);
                perpetrator.MaximumAge = int.Parse(ages[1]);

                perpetrators.Add(perpetrator);
            }

            return perpetrators;
        }

        private IEnumerable<Vehicle> GetVehicles(string json)
        {
            var vehiclesJson = json != null ? JsonConvert.DeserializeObject<JToken>(json) : new JArray();
            
            return vehiclesJson.Select(vehicleJson => new Vehicle
            {
                Brand = vehicleJson.Value<string>("Brand"),
                AdditionalFeatures = vehicleJson.Value<string>("AdditionalFeatures"),
                Color = vehicleJson.Value<string>("Color"),
                NumberPlate = vehicleJson.Value<string>("NumberPlate"),
                VehicleType = (VehicleTypeEnum) Enum.Parse(typeof (VehicleTypeEnum), vehicleJson.Value<string>("Type") ?? "Other", true)
            });
        }
    }
}
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
            report.StolenObject = viewModel.StolenObject ;
            report.DateOfTheft = viewModel.DateOfTheft.Add(viewModel.TimeOfTheft.TimeOfDay);
            report.Description = viewModel.Description;
            report.Vehicles = GetVehicles(viewModel.Vehicles);
        }

        public void Modify(Report report, DrugsViewModel viewModel)
        {
            report.DrugsAction = viewModel.Action;
            report.Description = viewModel.Description;
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
        }

        public void Modify(Report report, BullyingViewModel viewModel)
        {
            report.Description = viewModel.Description;
            report.VictimName = viewModel.VictimName;
        }
        public void Modify(Report report, VictimViewModel viewModel)
        {
            report.Victim = viewModel.Victim;
        }
        public void Modify(Report report, OtherViewModel viewModel)
        {
            report.Description = viewModel.Description;
        }

        public void Modify(Report report, PerpetratorViewModel viewModel)
        {
            if (report.Perpetrator == null)
            {
                report.Perpetrator = new Perpetrator();
            }
            if (viewModel.AgeRange != null)
            {
                var values = viewModel.AgeRange.Split('-');
                var minimumAge = Convert.ToInt32(values[0]);
                var maximumAge = Convert.ToInt32(values[1]);
                report.Perpetrator.MinimumAge = minimumAge;
                report.Perpetrator.MaximumAge = maximumAge;
            }
            report.Perpetrator.Name = viewModel.Name;
            report.Perpetrator.Clothing = viewModel.Clothing;
            report.Perpetrator.Sex = viewModel.Sex;
            report.Perpetrator.SkinColor = viewModel.SkinColor;
            report.Perpetrator.UniqueProperties = viewModel.UniqueProperties;
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

        public void Create(Report report, EditDoneViewModel viewModel)
        {
            viewModel.Category = report.Category;
            viewModel.Description = report.Description;
            viewModel.DrugsAction = report.DrugsAction;
            viewModel.FighterCount = report.FighterCount;
            viewModel.IsUnconscious = report.IsUnconscious;
            viewModel.DateOfTheft = report.DateOfTheft;
            viewModel.StolenObject = report.StolenObject;
            viewModel.Victim = report.Victim;
            viewModel.VictimName = report.VictimName;
            viewModel.IsWeaponPresent = report.IsWeaponPresent;
            viewModel.WeaponLocation = report.WeaponLocation;
            viewModel.WeaponType = report.WeaponType;
            viewModel.Location = report.Location;
            viewModel.Perpetrator = report.Perpetrator;
            viewModel.Vehicles = report.Vehicles;
            viewModel.Contact = report.Contact;
        }

        public void Modify(Report report, EditDoneViewModel viewModel)
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
            if(viewModel.WeaponType == "Anders")
            {
                report.WeaponType = viewModel.OtherType;
            }
            report.Location = viewModel.Location;
            report.Perpetrator = viewModel.Perpetrator;
            report.Vehicles = viewModel.Vehicles;

            if (viewModel.Contact.Name != null || viewModel.Contact.PhoneNumber != null || viewModel.Contact.EmailAddress != null)
            {
                if (report.Contact == null)
                {
                    report.Contact = new Contact();
                }
                report.Contact = viewModel.Contact;
            }
            else
            {
                report.Contact = null;
            }
        }

        private IEnumerable<Vehicle> GetVehicles(string json)
        {
            var vehiclesJson = JsonConvert.DeserializeObject<JToken>(json);

            return vehiclesJson.Select(vehicleJson => new Vehicle
            {
                Brand = vehicleJson.Value<string>("Brand"),
                AdditionalFeatures = vehicleJson.Value<string>("AdditionalFeatures"),
                Color = vehicleJson.Value<string>("Color"),
                NumberPlate = vehicleJson.Value<string>("NumberPlate"),
                VehicleType = (VehicleTypeEnum) Enum.Parse(typeof (VehicleTypeEnum), 
                vehicleJson.Value<string>("Type"), true)
            });
        }
    }
}
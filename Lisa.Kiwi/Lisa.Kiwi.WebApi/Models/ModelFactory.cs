using System.Collections.Generic;
using System.Linq;

namespace Lisa.Kiwi.WebApi
{
    internal class ModelFactory
    {
        public Report Create(ReportData reportData)
        {
            return new Report
            {
                Id = reportData.Id,
                Category = reportData.Category,
                IsVisible = reportData.IsVisible,
                IsDeleted = reportData.IsDeleted,
                Status = reportData.Status,
                Created = reportData.Created,
                Modified = reportData.Modified,
                Description = reportData.Description,
                IsUnconscious = reportData.IsUnconscious,

                Victim = reportData.Victim,
                VictimName = reportData.VictimName,

                WeaponType = reportData.WeaponType,
                WeaponTypeOther = reportData.WeaponTypeOther,
                WeaponLocation = reportData.WeaponLocation,

                FighterCount = reportData.FighterCount,
                IsWeaponPresent = reportData.IsWeaponPresent,

                DrugsAction = reportData.DrugsAction,

                StolenObject = reportData.StolenObject,
                DateOfTheft = reportData.DateOfTheft,

                Location = reportData.Location != null ? Create(reportData.Location) : null,
                Perpetrators = reportData.Perpetrators != null ? Create(reportData.Perpetrators) : null,
                Contact = reportData.Contact != null ? Create(reportData.Contact) : null,
                Vehicles = reportData.Vehicles != null ? Create(reportData.Vehicles) : null
            };
        }

        private Location Create(LocationData locationData)
        {
            return new Location
            {
                Building = locationData.Building,
                Description = locationData.Description,
            };
        }

        private IEnumerable<Perpetrator> Create(IEnumerable<PerpetratorData> perpetratorData)
        {
            return perpetratorData.Select(p => new Perpetrator
            {
                Clothing = p.Clothing,
                MinimumAge = p.MinimumAge,
                MaximumAge = p.MaximumAge,
                Name = p.Name,
                Sex = p.Sex,
                SkinColor = p.SkinColor,
                UniqueProperties = p.UniqueProperties
            });
        }

        private IEnumerable<Vehicle> Create(IEnumerable<VehicleData> vehicleData)
        {
            return vehicleData.Select(v => new Vehicle
            {
                Brand = v.Brand,
                AdditionalFeatures = v.AdditionalFeatures,
                Color = v.Color,
                NumberPlate = v.NumberPlate,
                VehicleType = v.VehicleType
            });
        }

        private Contact Create(ContactData contactData)
        {
            return new Contact
            {
                EmailAddress = contactData.EmailAddress,
                Name = contactData.Name,
                PhoneNumber = contactData.PhoneNumber
            };
        }
    }
}
using Lisa.Kiwi.WebApi;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web
{
    public class EditDoneViewModel
    {
        public string Category { get; set; }
        public string Description { get; set; }

        //FirstAid
        public bool? IsUnconscious { get; set; }

        //Theft
        public string StolenObject { get; set; }
        public DateTime? DateOfTheft { get; set; }

        //Drugs
        public string DrugsAction { get; set; }

        //Fight
        public int? FighterCount { get; set; }
        public bool? IsWeaponPresent { get; set; }

        //Weapons
        public string WeaponType { get; set; }
        public string WeaponLocation { get; set; }

        //Bullying
        public string VictimName { get; set; }
        public string Victim { get; set; }

        //Vehicle
        public string Brand { get; set; }
        public string NumberPlate { get; set; }
        public string Color { get; set; }
        public string AdditionalFeatures { get; set; }
        public VehicleTypeEnum? VehicleType { get; set; }

        //Perpetrator
        public string PerpetratorName { get; set; }
        public SexEnum? Sex { get; set; }
        public SkinColorEnum? SkinColor { get; set; }
        public string Clothing { get; set; }
        public int? MinimumAge { get; set; }
        public int? MaximumAge { get; set; }
        public string UniqueProperties { get; set; }

        //Contact
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        //Location
        public string Building { get; set; }
        public string LocationDescription { get; set; }
    }
}
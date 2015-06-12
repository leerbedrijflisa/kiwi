using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.WebApi
{
    [Table("Reports")]
    public class ReportData
    {
        public ReportData()
        {
            Created = DateTimeOffset.Now;
            IsVisible = true;
        }

        public int Id { get; set; }

        public string Category { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDeleted { get; set; }
        public StatusEnum Status { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }

        public string Description { get; set; }
        
        //First Aid
        public bool? IsUnconscious { get; set; }

        //Bullying
        public string Victim { get; set; }
        public string VictimName { get; set; }

        //Weapons
        public string WeaponType { get; set; }
        public string WeaponLocation { get; set; }

        //Fight
        public int? FighterCount { get; set; }
        public bool? IsWeaponPresent { get; set; }

        //Drugs
        public string DrugsAction { get; set; }

        //Theft
        public string StolenObject { get; set; }
        public DateTime? DateOfTheft { get; set; }

        
        public virtual LocationData Location { get; set; }
        public virtual PerpetratorData Perpetrator { get; set; }
        public virtual ContactData Contact { get; set; }
        public virtual VehicleData Vehicle { get; set; }
    }
}
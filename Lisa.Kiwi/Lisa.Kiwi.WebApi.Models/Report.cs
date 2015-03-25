using System;

namespace Lisa.Kiwi.WebApi
{
    public class Report
    {
        public Report()
        {
            Created = DateTimeOffset.Now;
            IsVisible = true;
        }

        public int Id { get; set; }
        public string Category { get; set; }
        public bool IsVisible { get; set; }
        public Status Status { get; set; }
        public DateTimeOffset Created { get; set; }
        public string AnonymousToken { get; set; }

        //Shared
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

        //Nuisance
        //Bullying
        public string Victim { get; set; }

        //Other

        public Location Location { get; set; }
        public Contact Contact { get; set; }
        public Perpetrator Perpetrator { get; set; }
        public Vehicle Vehicle { get; set; }
    }

    public enum Status
    {
        Open,
        Solved,
        Transferred
    }
}
using System.Linq;
using Lisa.Kiwi.WebApi;

namespace Lisa.Kiwi.Web
{
    public class DoneViewModel : Report
    {
        public DoneViewModel() { }

        public DoneViewModel(Report report)
        {
            Location = report.Location;
            Category = report.Category;
            Created = report.Created;
            Modified = report.Modified;
            Description = report.Description;
            IsUnconscious = report.IsUnconscious;
            StolenObject = report.StolenObject;
            DateOfTheft = report.DateOfTheft;
            DrugsAction = report.DrugsAction;
            FighterCount = report.FighterCount;
            IsWeaponPresent = report.IsWeaponPresent;
            if (!_validWeapons.Contains(report.WeaponType))
            {
                WeaponType = "Anders";
                WeaponTypeOther = report.WeaponType;
            }
            else
            {
                WeaponType = report.WeaponType;
            }
            WeaponLocation = report.WeaponLocation;
            VictimName = report.VictimName;
            Victim = report.Victim;
            Location = report.Location;
            Contact = report.Contact;
            Perpetrators = report.Perpetrators;
            Vehicles = report.Vehicles;
            Files = report.Files;
        }
        public string WeaponTypeOther { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; }

        private static string[] _validWeapons =
        {
            "Mes",
            "Boksbeugel",
            "Vuurwapen",
            "Pepperspray"
        };
    }
}
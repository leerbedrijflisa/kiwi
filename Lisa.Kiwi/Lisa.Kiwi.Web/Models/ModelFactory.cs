using Lisa.Kiwi.WebApi;
using System;

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
            report.Location.Description = viewModel.Location;
        }

        public void Modify(Report report, FirstAidViewModel viewModel)
        {
            report.IsUnconscious = viewModel.IsUnconscious == "Ja";
        }

        public void Modify(Report report, TheftViewModel viewModel)
        {
            report.StolenObject = viewModel.StolenObject ;
            report.DateOfTheft = viewModel.DateOfTheft.Add(viewModel.TimeOfTheft.TimeOfDay);
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
        }

        public void Modify(Report report, WeaponViewModel viewModel)
        {
            report.WeaponType = viewModel.Type;
            if (viewModel.Type == "Anders")
            {
                report.WeaponType = viewModel.OtherType;
            }
            report.WeaponLocation = viewModel.Location;
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
                string[] values = viewModel.AgeRange.Split('-');
                int MinimumAge = Convert.ToInt32(values[0]);
                int MaximumAge = Convert.ToInt32(values[1]);
                report.Perpetrator.MinimumAge = MinimumAge;
                report.Perpetrator.MaximumAge = MaximumAge;
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
    }
}
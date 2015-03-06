using Lisa.Kiwi.WebApi;
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
            report.Location.Building = viewModel.Building;
            report.Location.Description = viewModel.Location;
        }

        public void Modify(Report report, FirstAidViewModel viewModel)
        {
            report.IsUnconscious = viewModel.IsUnconscious;
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
            report.WeaponLocation = viewModel.Location;
        }

        public void Modify(Report report, NuisanceViewModel viewModel)
        {

        }

        public void Modify(Report report, BullyingViewModel viewModel)
        {
            report.Victim = viewModel.Victim;
            report.Description = viewModel.Description;
        }

        public void Modify(Report report, OtherViewModel viewModel)
        {

        }

        public void Modify(Report report, PerpetratorViewModel viewModel)
        {
            report.Perpetrator.Name = viewModel.Name;
            report.Perpetrator.Clothing = viewModel.Clothing;
            report.Perpetrator.MinimumAge = viewModel.MinimumAge;
            report.Perpetrator.MaximumAge = viewModel.MaximumAge;
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
            report.Contact.Name = viewModel.Name;
            report.Contact.PhoneNumber = viewModel.PhoneNumber;
            report.Contact.EmailAddress = viewModel.EmailAddress;
        }
    }
}
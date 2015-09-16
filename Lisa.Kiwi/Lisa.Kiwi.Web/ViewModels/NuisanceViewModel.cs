using System.ComponentModel.DataAnnotations;
using Resources;

namespace Lisa.Kiwi.Web
{
    public class NuisanceViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Display(Name = "NuisanceDescription", ResourceType = typeof(ReportProperties))]
        public string Description { get; set; }

        public string Vehicles { get; set; }
        public string Perpetrators { get; set; }
    }
}
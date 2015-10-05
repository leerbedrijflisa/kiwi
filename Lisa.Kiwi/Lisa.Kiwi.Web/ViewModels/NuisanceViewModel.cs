using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class NuisanceViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Description { get; set; }

        public string Vehicles { get; set; }
        public string Perpetrators { get; set; }
    }
}
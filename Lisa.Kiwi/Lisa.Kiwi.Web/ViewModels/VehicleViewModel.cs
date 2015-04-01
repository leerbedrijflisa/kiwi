using System.ComponentModel.DataAnnotations;
using Resources;

namespace Lisa.Kiwi.Web
{
    public class VehicleViewModel
    {
        public bool HasVehicle { get; set; }

        [Display(Name = "VehicleColor", ResourceType = typeof(ReportProperties))]
        public string Color { get; set; }

        [Display(Name = "VehicleNumberPlate", ResourceType = typeof(ReportProperties))]
        public string NumberPlate { get; set; }

        [Display(Name = "VehicleBrand", ResourceType = typeof(ReportProperties))]
        public string Brand { get; set; }

        [Display(Name = "VehicleAdditionalFeatures", ResourceType = typeof(ReportProperties))]
        public string AdditionalFeatures { get; set; }
    }
}
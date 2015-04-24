using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;
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

        [Display(Name = "VehicleType", ResourceType = typeof(ReportProperties))]
        public VehicleTypeEnum VehicleType { get; set; }

        public IEnumerable<SelectListItem> VehicleTypes
        {
            get
            {
                return new[]
                {
                    new SelectListItem
                    {
                        Text = Resources.VehicleTypes.Car,
                        Value = "Car"
                    },
                    new SelectListItem
                    {
                        Text = Resources.VehicleTypes.Bicycle,
                        Value = "Bicycle"
                    },
                    new SelectListItem
                    {
                        Text = Resources.VehicleTypes.Moped,
                        Value = "Moped"
                    },
                    new SelectListItem
                    {
                        Text = Resources.VehicleTypes.Motorcycle,
                        Value = "Motorcycle"
                    },
                    new SelectListItem
                    {
                        Text = Resources.VehicleTypes.Unknown,
                        Value = "Unknown"
                    }
                };
            }
        } 
    }
}
using System.Collections.Generic;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;

namespace Lisa.Kiwi.Web
{
    public class VehicleViewModel
    {
        public string Color { get; set; }
        public string NumberPlate { get; set; }
        public string Brand { get; set; }
        public string AdditionalFeatures { get; set; }
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
                        Text = Resources.VehicleTypes.Other,
                        Value = "Other"
                    }
                };
            }
        } 
    }
}
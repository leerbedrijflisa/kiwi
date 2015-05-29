using Lisa.Kiwi.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web
{
    public class EditDoneViewModel : Report
    {
        public string AgeRange { get; set; }
        public string OtherType { get; set; }
        public IEnumerable<SelectListItem> SkinColors
        {
            get
            {
                return new[]
                {
                    new SelectListItem
                    {
                        Text = Resources.SkinColor.Light,
                        Value = "Light"
                    },
                    new SelectListItem
                    {
                        Text = Resources.SkinColor.Tanned,
                        Value = "Tanned"
                    },
                    new SelectListItem
                    {
                        Text = Resources.SkinColor.Dark,
                        Value = "Dark"
                    },
                };
            }
        }

        public IEnumerable<SelectListItem> AgeRanges
        {
            get
            {
                return new[]
                {
                    new SelectListItem
                    {
                        Text = Resources.Perpetrator.YoungerThanTwelve,
                        Value = "0-12"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Perpetrator.BetweenTwelveAndTwenty,
                        Value = "12-20"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Perpetrator.BetweenTwentyOneAndThirtyFive,
                        Value = "21-35"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Perpetrator.OlderThanThirtyFive,
                        Value = "35-99"
                    },
                };
            }
        }

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

        public IEnumerable<SelectListItem> Types
        {
            get
            {
                return new[]
                {
                    new SelectListItem
                    {
                        Text = "Mes",
                        Value = "Mes"
                    },
                    new SelectListItem
                    {
                        Text = "Boksbeugel",
                        Value = "Boksbeugel"
                    },
                    new SelectListItem
                    {
                        Text = "Vuurwapen",
                        Value = "Vuurwapen"
                    },
                    new SelectListItem
                    {
                        Text = "Pepperspray",
                        Value = "Pepperspray"
                    },
                    new SelectListItem
                    {
                        Text = "Anders",
                        Value = "Anders"
                    },
                };
            }
        }


    }
}
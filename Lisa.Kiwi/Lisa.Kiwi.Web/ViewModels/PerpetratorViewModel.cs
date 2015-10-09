using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Lisa.Kiwi.Web.Resources;
using Lisa.Kiwi.WebApi;

namespace Lisa.Kiwi.Web
{
    public class PerpetratorViewModel
    {
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public SexEnum Sex { get; set; }

        public SkinColorEnum SkinColor { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Clothing { get; set; }

        public string AgeRange { get; set; }

        public string UniqueProperties { get; set; }

        public IEnumerable<SelectListItem> SkinColors
        {
            get
            {
                return new []
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

        public IEnumerable<SelectListItem> SexList
        {
            get
            {
                return new[]
                {
                    new SelectListItem
                    {
                        Text = Resources.Sex.Unknown,
                        Value = "Unknown"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Sex.Male,
                        Value = "Male"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Sex.Female,
                        Value = "Female"
                    },
                };
            }
        }

        public IEnumerable<SelectListItem> AgeRanges
        {
            get
            {
                return new []
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
    }
}
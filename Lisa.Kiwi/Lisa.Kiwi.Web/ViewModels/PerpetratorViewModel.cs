using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;
using Perpetrator = Resources.Perpetrator;

namespace Lisa.Kiwi.Web
{
    public class PerpetratorViewModel
    {
        [DisplayName("Wat is de naam van de dader?")]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat is het geslacht van de dader? *")]
        public SexEnum Sex { get; set; }

        [DisplayName("Wat is de huidskleur van de dader?")]
        public SkinColorEnum SkinColor { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat voor kleding draagt de dader? *")]
        public string Clothing { get; set; }

        [DisplayName("Welke leeftijd heeft de dader?")]
        public string AgeRange { get; set; }

        [DisplayName("Zijn er opvallende dingen te zien aan de dader?")]
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
                        Text = "Onbekend",
                        Value = "0"
                    },
                    new SelectListItem
                    {
                        Text = "Man",
                        Value = "1"
                    },
                    new SelectListItem
                    {
                        Text = "Vrouw",
                        Value = "2"
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
                        Text = Perpetrator.YoungerThanTwelve,
                        Value = "0-12"
                    },
                    new SelectListItem
                    {
                        Text = Perpetrator.BetweenTwelveAndTwenty,
                        Value = "12-20"
                    },
                    new SelectListItem
                    {
                        Text = Perpetrator.BetweenTwentyOneAndThirtyFive,
                        Value = "21-35"
                    },
                    new SelectListItem
                    {
                        Text = Perpetrator.OlderThanThirtyFive,
                        Value = "35-99"
                    },
                };
            }
        }
    }
}
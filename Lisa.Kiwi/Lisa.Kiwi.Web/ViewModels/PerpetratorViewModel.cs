using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;

namespace Lisa.Kiwi.Web
{
    public class PerpetratorViewModel
    {
        [DisplayName("Wat is de naam van de dader?")]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat is het geslacht van de dader?")]
        public SexEnum Sex { get; set; }

        [DisplayName("Wat is de huidskleur van de dader?")]
        public string SkinColor { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat voor kleding draagt de dader?")]
        public string Clothing { get; set; }

        [DisplayName("Tussen welke leeftijd is de dader?")]
        public string AgeRange { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Zijn er opvallende dingen te zien aan de dader?")]
        public string UniqueProperties { get; set; }

        public IEnumerable<SelectListItem> SkinColors
        {
            get
            {
                return new SelectListItem[]
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
                // TODO: use resource files for Text-field, no.
                return new SelectListItem[]
                {
                    new SelectListItem
                    {
                        Text = "12-16",
                        Value = "12-16"
                    },
                    new SelectListItem
                    {
                        Text = "16-18",
                        Value = "16-18"
                    },
                    new SelectListItem
                    {
                        Text = "18-20",
                        Value = "18-20"
                    },
                    new SelectListItem
                    {
                        Text = "20-22",
                        Value = "20-22"
                    },
                    new SelectListItem
                    {
                        Text = "22-30",
                        Value = "22-30"
                    },
                };
            }
        }
    }
}
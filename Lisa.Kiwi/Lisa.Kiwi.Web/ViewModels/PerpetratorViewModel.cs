using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web
{
    public class PerpetratorViewModel
    {
        [DisplayName("Wat is de naam van de dader?")]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat is het geslacht van de dader?")]
        public int Sex { get; set; }

        [DisplayName("Wat is de huidskleur van de dader?")]
        public string SkinColor { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat voor kleding draagt de dader?")]
        public string Clothing { get; set; }

        [DisplayName("Tussen welke leeftijd is de dader?")]
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Zijn er opvallende dingen te zien aan de dader?")]
        public string UniqueProperties { get; set; }

        public IEnumerable<SelectListItem> SkinColors
        {
            get
            {
                // TODO: use resource files for Text-field
                return new SelectListItem[]
                {
                    new SelectListItem
                    {
                        Text = "Blank",
                        Value = "Light"
                    },
                    new SelectListItem
                    {
                        Text = "Licht getint",
                        Value = "Tanned"
                    },
                    new SelectListItem
                    {
                        Text = "Donker",
                        Value = "Dark"
                    },
                };
            }
        }
    }
}
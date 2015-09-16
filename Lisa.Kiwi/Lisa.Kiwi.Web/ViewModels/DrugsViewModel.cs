using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web
{
    public class DrugsViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat heb je gezien?")]
        public string Action { get; set; }

        [DisplayName("Wat kun je er verder over vertellen?")]
        public string Description { get; set; }

        public string Vehicles { get; set; }

        public IEnumerable<SelectListItem> DrugsActions
        {
            get
            {
                return new[]
                {
                    new SelectListItem
                    {
                        Text = "Drugsgebruik",
                        Value = "Ùsing"
                    },
                    new SelectListItem
                    {
                        Text = "Dealen van drugs",
                        Value = "Dealing"
                    },
                };
            }
        }
    }
}
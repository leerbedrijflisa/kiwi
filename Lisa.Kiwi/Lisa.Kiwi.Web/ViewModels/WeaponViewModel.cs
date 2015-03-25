using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web
{
    public class WeaponViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat voor wapen is het?")]
        public string Type { get; set; }

        public string OtherType { get; set; }
        

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Waar is het wapen?")]
        public string Location { get; set; }

        public IEnumerable<SelectListItem> Types
        {
            get
            {
                // TODO: use resource files for Text-field
                // Can't make a resource file, atleast a functional one, as this is saved in dutch
                return new []
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
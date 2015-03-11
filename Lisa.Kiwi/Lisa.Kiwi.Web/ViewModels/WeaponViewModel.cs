using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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
                return new SelectListItem[]
                {
                    new SelectListItem
                    {
                        Text = "Mes",
                        Value = "Knife"
                    },
                    new SelectListItem
                    {
                        Text = "Boksbeugel",
                        Value = "Brass Knuckles"
                    },
                    new SelectListItem
                    {
                        Text = "Vuurwapen",
                        Value = "Firearm"
                    },
                    new SelectListItem
                    {
                        Text = "Pepperspray",
                        Value = "Pepperspray"
                    },
                    new SelectListItem
                    {
                        Text = "Anders",
                        Value = "Other"
                    },
                };
            }
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class DrugsViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Action { get; set; }

        public string Description { get; set; }

        public string Vehicles { get; set; }
        public string Perpetrators { get; set; }

        public IEnumerable<SelectListItem> DrugsActions
        {
            get
            {
                return new[]
                {
                    new SelectListItem
                    {
                        Text = "Drugsgebruik",
                        Value = "Using"
                    },
                    new SelectListItem
                    {
                        Text = "Dealen van drugs",
                        Value = "Dealing"
                    }
                };
            }
        }
    }
}
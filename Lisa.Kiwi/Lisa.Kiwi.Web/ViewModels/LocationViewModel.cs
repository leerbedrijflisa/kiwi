using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web
{
    public class LocationViewModel
    {
        [Required]
        public string Building { get; set; }

        [Required]
        public string Location { get; set; }

        public IEnumerable<SelectListItem> Buildings
        {
            get
            {
                return new SelectListItem[]
                {
                    new SelectListItem
                    {
                        Text = "Azzurro",
                        Value = "Azzurro"
                    },

                    new SelectListItem
                    {
                        Text = "Verde",
                        Value = "Verde"
                    }
                };
            }
        }
    }
}
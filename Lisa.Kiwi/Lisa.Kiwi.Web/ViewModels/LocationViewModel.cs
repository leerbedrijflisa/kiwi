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
                        Text = "Buiten (Openbare Weg)",
                        Value = "Buiten"
                    },

                    new SelectListItem
                    {
                        Text = "DVC Lilla",
                        Value = "Lilla"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Marrone",
                        Value = "Marrone"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Rosa",
                        Value = "Rosa"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Verde",
                        Value = "Verde"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Giallo",
                        Value = "Giallo"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Indaco",
                        Value = "Indaco"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Bianco",
                        Value = "Bianco"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Ocra",
                        Value = "Ocra"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Arcobaleno",
                        Value = "Arcobaleno"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Celeste",
                        Value = "Celeste"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Azzurro",
                        Value = "Azzurro"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Romboutslaan 40",
                        Value = "Romboutslaan"
                    },
                    new SelectListItem
                    {
                        Text = "Duurzaamheidsfabriek",
                        Value = "Duurzaamheidsfabriek"
                    },
                    new SelectListItem
                    {
                        Text = "Drechtsteden college",
                        Value = "Drechtsteden"
                    },
                    new SelectListItem
                    {
                        Text = "Samenwerkingsgebouw",
                        Value = "Samenwerkingsgebouw"
                    },
                    new SelectListItem
                    {
                        Text = "Wartburg college",
                        Value = "Wartburg"
                    },
                    new SelectListItem
                    {
                        Text = "Sporthal",
                        Value = "Sporthal"
                    },
                    new SelectListItem
                    {
                        Text = "Klimhal",
                        Value = "Klimhal"
                    },
                    new SelectListItem
                    {
                        Text = "Ds. Bogermanschool",
                        Value = "Bogermanschool"
                    },
                    new SelectListItem
                    {
                        Text = "SchippersinternaatAppartementen",
                        Value = "Schippersinternaat"
                    },
                    new SelectListItem
                    {
                        Text = "Brandweerkazerne",
                        Value = "Brandweerkazerne"
                    },
                    new SelectListItem
                    {
                        Text = "Veiligheidsregio",
                        Value = "Veiligheidsregio"
                    },
                    new SelectListItem
                    {
                        Text = "Betaalde parkeerplaats",
                        Value = "Parkeerplaats"
                    },
                    new SelectListItem
                    {
                        Text = "DVC media",
                        Value = "DVC media"
                    },
                    new SelectListItem
                    {
                        Text = "Orthodontist",
                        Value = "Orthodontist"
                    },
                };
            }
        }
    }
}
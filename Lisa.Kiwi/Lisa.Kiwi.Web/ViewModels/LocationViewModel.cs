using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web
{
    public class LocationViewModel
    {
        // TODO: use resource files for error messages and display names

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Gebouw")]
        public string Building { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Locatie")]
        public string Location { get; set; }

        public IEnumerable<SelectListItem> Buildings
        {
            get
            {
                // TODO: use resource files for Text-field
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
                        Value = "DVC Lilla"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Marrone",
                        Value = "DVC Marrone"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Rosa",
                        Value = "DVC Rosa"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Verde",
                        Value = "DVC Verde"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Giallo",
                        Value = "DVC Giallo"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Indaco",
                        Value = "DVC Indaco"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Bianco",
                        Value = "DVC Bianco"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Ocra",
                        Value = "DVC Ocra"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Arcobaleno",
                        Value = "DVC Arcobaleno"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Celeste",
                        Value = "DVC Celeste"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Azzurro",
                        Value = "DVC Azzurro"
                    },
                    new SelectListItem
                    {
                        Text = "DVC Romboutslaan 40",
                        Value = "DVC Romboutslaan"
                    },
                    new SelectListItem
                    {
                        Text = "Duurzaamheidsfabriek",
                        Value = "Duurzaamheidsfabriek"
                    },
                    new SelectListItem
                    {
                        Text = "Appartementen",
                        Value = "Appartementen"
                    },
                    new SelectListItem
                    {
                        Text = "Drechtsteden college",
                        Value = "Drechtsteden college"
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
                        Text = "Schippersinternaat",
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
                    new SelectListItem
                    {
                        Text = "Parkeerplaats Ocra",
                        Value = "Parkeerplaats Ocra"
                    },
                    new SelectListItem
                    {
                        Text = "Parkeerplaats Duurzaamheidsfabriek",
                        Value = "Parkeerplaats Duurzaamheidsfabriek"
                    },
                    new SelectListItem
                    {
                        Text = "Parkeerplaats Brandweerkazerne",
                        Value = "Parkeerplaats Brandweerkazerne"
                    },
                };
            }
        }
    }
}
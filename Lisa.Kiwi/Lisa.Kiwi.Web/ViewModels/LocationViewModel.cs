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
                return new []
                {
                    new SelectListItem
                    {
                        
                        Text = Resources.Buildings.Buiten,
                        Value = "Buiten"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCLilla,
                        Value = "DVC Lilla"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCMarrone,
                        Value = "DVC Marrone"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCRosa,
                        Value = "DVC Rosa"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCVerde,
                        Value = "DVC Verde"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCGiallo,
                        Value = "DVC Giallo"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCIndaco,
                        Value = "DVC Indaco"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCBianco,
                        Value = "DVC Bianco"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCOcra,
                        Value = "DVC Ocra"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCArcobaleno,
                        Value = "DVC Arcobaleno"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCCeleste,
                        Value = "DVC Celeste"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCAzzurro,
                        Value = "DVC Azzurro"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCRomboutslaan,
                        Value = "DVC Romboutslaan"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Duurzaamheidsfabriek,
                        Value = "Duurzaamheidsfabriek"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Appartementen,
                        Value = "Appartementen"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Drechtstedencollege,
                        Value = "Drechtsteden college"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Samenwerkingsgebouw,
                        Value = "Samenwerkingsgebouw"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Wartburg,
                        Value = "Wartburg"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Sporthal,
                        Value = "Sporthal"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Klimhal,
                        Value = "Klimhal"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Bogermanschool,
                        Value = "Bogermanschool"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Schippersinternaat,
                        Value = "Schippersinternaat"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Brandweerkazerne,
                        Value = "Brandweerkazerne"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Veiligheidsregio,
                        Value = "Veiligheidsregio"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Parkeerplaats,
                        Value = "Parkeerplaats"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.DVCmedia,
                        Value = "DVC media"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.Orthodontist,
                        Value = "Orthodontist"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.ParkeerplaatsOcra,
                        Value = "Parkeerplaats Ocra"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.ParkeerplaatsDuurzaamheidsfabriek,
                        Value = "Parkeerplaats Duurzaamheidsfabriek"
                    },
                    new SelectListItem
                    {
                        Text = Resources.Buildings.ParkeerplaatsBrandweerkazerne,
                        Value = "Parkeerplaats Brandweerkazerne"
                    },
                };
            }
        }
    }
}
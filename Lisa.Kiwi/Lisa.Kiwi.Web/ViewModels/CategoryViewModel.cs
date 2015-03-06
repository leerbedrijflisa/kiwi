using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web
{
    public class CategoryViewModel
    {
        [Required]
        public string Category { get; set; }

        public IEnumerable<SelectListItem> Categories
        {
            get
            {
                return new SelectListItem[]
                {
                    // TODO: use resource files for Text-fields
                    new SelectListItem
                    {
                        Text = "EHBO",
                        Value = "FirstAid"
                    },

                    new SelectListItem
                    {
                        Text = "Vechtpartij",
                        Value = "Fight"
                    },

                    new SelectListItem
                    {
                        Text = "Wapens",
                        Value = "Weapons"
                    },

                    new SelectListItem
                    {
                        Text = "Drugs",
                        Value = "Drugs"
                    },

                    new SelectListItem
                    {
                        Text = "Diefstal",
                        Value = "Theft"
                    },

                    new SelectListItem
                    {
                        Text = "Pesten",
                        Value = "Bullying"
                    },

                    new SelectListItem
                    {
                        Text = "Overlast",
                        Value = "Nuisance"
                    },

                    new SelectListItem
                    {
                        Text = "Overige",
                        Value = "Other"
                    }
                };
            }
        }
    }
}
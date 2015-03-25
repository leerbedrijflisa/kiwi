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
                    new SelectListItem
                    {
                        Text = Resources.Categories.FirstAid,
                        Value = "FirstAid"
                    },

                    new SelectListItem
                    {
                        Text = Resources.Categories.Fight,
                        Value = "Fight"
                    },

                    new SelectListItem
                    {
                        Text = Resources.Categories.Weapons,
                        Value = "Weapons"
                    },

                    new SelectListItem
                    {
                        Text = Resources.Categories.Drugs,
                        Value = "Drugs"
                    },

                    new SelectListItem
                    {
                        Text = Resources.Categories.Theft,
                        Value = "Theft"
                    },

                    new SelectListItem
                    {
                        Text = Resources.Categories.Bullying,
                        Value = "Bullying"
                    },

                    new SelectListItem
                    {
                        Text = Resources.Categories.Nuisance,
                        Value = "Nuisance"
                    },

                    new SelectListItem
                    {
                        Text = Resources.Categories.Other,
                        Value = "Other"
                    }
                };
            }
        }
    }
}
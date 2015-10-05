using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class CategoryViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Category { get; set; }

        public IEnumerable<SelectListItem> Categories
        {
            get
            {
                return new []
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
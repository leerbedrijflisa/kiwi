using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web.ViewModels
{
    public class NuisanceViewModel
    {
        [Required]
        public string Description { get; set; }
    }
}
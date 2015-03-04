using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web.ViewModels
{
    public class PerpetratorViewModel
    {
        public string Name { get; set; }

        [Required]
        public int Sex { get; set; }

        public string SkinColor { get; set; }

        [Required]
        public string Clothing { get; set; }

        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }

        [Required]
        public string UniqueProperties { get; set; }
    }
}
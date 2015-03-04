using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web.ViewModels
{
    public class PerpetratorViewModel
    {
        public string PerpetratorName { get; set; }

        [Required]
        public char PerpetratorGender { get; set; }

        public string PerpetratorSkinColor { get; set; }

        [Required]
        public string PerpetratorClothing { get; set; }

        public int PerpetratorAge { get; set; }

        [Required]
        public string PerpetratorUniqueProperties { get; set; }
    }
}
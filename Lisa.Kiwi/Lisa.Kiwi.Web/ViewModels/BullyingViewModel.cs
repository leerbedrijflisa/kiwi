using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web.ViewModels
{
    public class BullyingViewModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Victim { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web
{
    public class FirstAidViewModel
    {
        [Required]
        public bool IsUnconscious { get; set; }
    }
}
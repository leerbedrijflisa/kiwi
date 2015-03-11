using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web
{
    public class FirstAidViewModel
    {
        public string IsUnconscious { get; set; }
        public string IsNotUnconscious { get; set; }
    }
}
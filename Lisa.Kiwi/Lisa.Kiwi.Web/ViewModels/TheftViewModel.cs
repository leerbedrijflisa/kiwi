using System;
using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class TheftViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string StolenObject { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public DateTime DateOfTheft { get; set; }
        
        public string Description { get; set; }
        public string Vehicles { get; set; }
        public string Perpetrators { get; set; }
    }
}
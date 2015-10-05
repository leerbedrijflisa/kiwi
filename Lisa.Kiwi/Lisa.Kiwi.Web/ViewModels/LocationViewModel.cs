using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class LocationViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Building { get; set; }
    }
}
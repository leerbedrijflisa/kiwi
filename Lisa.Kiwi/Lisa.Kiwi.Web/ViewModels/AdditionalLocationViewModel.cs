using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class AdditionalLocationViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string AdditionalLocation { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class BullyingViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string VictimName { get; set; }

        public string Perpetrators { get; set; }
    }
}
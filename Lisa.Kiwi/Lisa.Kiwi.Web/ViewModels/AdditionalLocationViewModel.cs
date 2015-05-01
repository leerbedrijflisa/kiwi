using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web
{
    public class AdditionalLocationViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Locatie")]
        public string AdditionalLocation { get; set; }
    }
}
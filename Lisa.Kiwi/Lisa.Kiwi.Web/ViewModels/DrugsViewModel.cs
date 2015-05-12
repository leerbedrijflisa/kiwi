using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web
{
    public class DrugsViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wordt er gedeald of gebruikt? *")]
        public string Action { get; set; }

        [DisplayName("Heeft u nog overige informatie die u wilt melden?")]
        public string Description { get; set; }
    }
}
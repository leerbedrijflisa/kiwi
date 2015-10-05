using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "UserName", ResourceType = typeof(DisplayNames))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(DisplayNames))]
        public string Password { get; set; }
    }
}
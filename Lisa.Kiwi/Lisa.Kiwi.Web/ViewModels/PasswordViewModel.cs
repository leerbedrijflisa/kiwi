using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class PasswordViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string NewPassword { get; set; }

        [Display(Name = "RepeatNewPassword", ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string NewPasswordRepeat { get; set; }
    }
}
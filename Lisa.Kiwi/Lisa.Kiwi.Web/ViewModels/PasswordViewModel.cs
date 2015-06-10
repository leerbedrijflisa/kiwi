using System.ComponentModel.DataAnnotations;
using Resources;

namespace Lisa.Kiwi.Web
{
    public class PasswordViewModel
    {
        public string Id { get; set; }
        [Display(Name = "UserName", ResourceType = typeof(DisplayNames))]
        [Required]
        public string Username { get; set; }

        [Display(Name = "NewPassword", ResourceType = typeof(DisplayNames))]
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Het wachtwoord moet minstens 6 tekens lang zijn.")]
        public string NewPassword { get; set; }

        [Display(Name = "RepeatNewPassword", ResourceType = typeof(DisplayNames))]
        [Required]
        public string NewPasswordRepeat { get; set; }
    }
}
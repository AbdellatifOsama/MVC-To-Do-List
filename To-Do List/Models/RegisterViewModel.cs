using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace To_Do_List.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(18)]
        [MinLength(2)]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public bool IsTermsAgreed { get; set; }
    }
}

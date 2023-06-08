using System.ComponentModel;

namespace To_Do_List.Models
{
    public class LoginViewModel
    {
        [DisplayName("Email Address")]
        public string? Email { get; set; }
        public string? Password { get; set; }
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}

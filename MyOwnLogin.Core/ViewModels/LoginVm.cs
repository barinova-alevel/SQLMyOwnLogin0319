using System.ComponentModel.DataAnnotations;

namespace MyOwnLogin.Core.ViewModels
{
    public class LoginVm
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
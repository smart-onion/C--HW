using System.ComponentModel.DataAnnotations;

namespace AspIdentityHW1.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }    
        public string? ReturnUrl { get; set; }
    }
}

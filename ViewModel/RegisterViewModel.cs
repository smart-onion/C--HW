using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace hw10.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username field required!")]
        [RegularExpression(@"^\w+$")]
        [StringLength(100, MinimumLength = 2)]
        [Remote("UsernameIsExist", "TvShow")]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [CreditCard(ErrorMessage = "Wrong credit card!")]
        public string CreditCard { get; set; }
        [Url]
        public string Website { get; set; }
        [Required(ErrorMessage = "Username field required!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Username field required!")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Range(18,100)]
        public uint Age { get; set; }

    }
}

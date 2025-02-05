using Azure.Identity;
using System.ComponentModel.DataAnnotations;

namespace AspIdentityHW1.ViewModels
{
    public class EditAccountViewModel
    {

        public string? Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email     { get; set; }
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

    }
}

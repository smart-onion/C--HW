using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.ViewModels;

public class ResetPasswordViewModel
{
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmNewPassword { get; set; }
    
    public string Email { get; set; }
    public string Token { get; set; }
}
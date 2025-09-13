using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string? Email { get; set; }
 
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
 
    [Required]
    [Compare("Password", ErrorMessage = "The passwords don't match")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string? PasswordConfirm { get; set; }
}
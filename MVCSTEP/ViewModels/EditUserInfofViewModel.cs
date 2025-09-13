using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.ViewModels;

public class EditUserInfofViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
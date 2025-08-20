using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.Models;

public class User
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    [Range(6,12)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
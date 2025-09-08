using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVCSTEP.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [ValidateNever]
    public Role Role { get; set; }
    [ValidateNever]
    public DateTime CreatedAt { get; set; }
}



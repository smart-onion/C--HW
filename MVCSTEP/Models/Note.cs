using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.Models;

public class Note
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Subject { get; set; }
    public string Body { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}
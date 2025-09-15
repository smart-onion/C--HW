using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.Models;

public class Publication
{
    [Key] public int Id { get; set; }
    [Required] public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PublicationDate { get; } = DateTime.UtcNow;
    public PublicationAccess PublicationAccess { get; set; }

    [Required] public string UserId { get; set; }
    [Required] public User User { get; set; }
}
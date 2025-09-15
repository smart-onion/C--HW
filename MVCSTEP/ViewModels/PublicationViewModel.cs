using MVCSTEP.Models;

namespace MVCSTEP.ViewModels;

public class PublicationViewModel
{
    public int? Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public PublicationAccess PublicationAccess { get; set; }
    public UserViewModel? Creator  { get; set; }
    public DateTime? PublicationDate { get; set; } = DateTime.UtcNow;
    
}
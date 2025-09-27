using System.ComponentModel.DataAnnotations;

namespace Kanban.Model;

public class Card
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string Status { get; set; } = null!;
    public int ListId { get; set; }
    public List List { get; set; } = null!;
    public DateTime UpdatedAt { get; set; } =  DateTime.UtcNow;
}
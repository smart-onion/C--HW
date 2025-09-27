using System.ComponentModel.DataAnnotations;

namespace Kanban.Model;

public class Board
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    public string Description { get; set; } = null!;
}
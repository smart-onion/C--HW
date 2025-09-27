namespace Kanban.Model;

public class List
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int BoardId { get; set; }
    public Board Board { get; set; } = null!;
}
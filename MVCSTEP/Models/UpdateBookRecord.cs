namespace MVCSTEP.Models;

public class UpdateBookRecord
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public DateTime Updated { get; set; } =  DateTime.Now;
}
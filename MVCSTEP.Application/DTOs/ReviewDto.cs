namespace MVCSTEP.Application.DTOs;

public class ReviewDto
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    public int ProductId { get; set; }
    
    public DateTime Created { get; set; } =  DateTime.UtcNow;
    public DateTime Updated { get; set; } =  DateTime.UtcNow;
    
    public string Comment { get; set; }
    
    public decimal Rating{get;set;}
}
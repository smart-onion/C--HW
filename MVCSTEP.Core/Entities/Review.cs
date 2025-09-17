using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.Core.Entities;

public record Review
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string UserId { get; set; }
    public User User { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public DateTime Created { get; set; } =  DateTime.UtcNow;
    public DateTime Updated { get; set; } =  DateTime.UtcNow;
    
    [Required]
    public string Comment { get; set; }
    
    [Range(0, 5)]
    [Required]
    public decimal Rating{get;set;}
}
using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.Core.Entities;

public record Product
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Product Name")]
    [MinLength(2)]
    [Required]
    public string Name { get; set; }

    [Required]
    [Range(0.01, int.MaxValue)]
    public decimal Price { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
    
    public IEnumerable<Review> Reviews { get; set; }
    
}
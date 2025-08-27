using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    [Range(minimum: 3, maximum: 24, ErrorMessage = "Name in range from 3 to 24 chars"), Display(Name = "Product Name")]
    public string Name { get; set; }
    [Required]
    [Display(Name = "Product Price")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Price must be between 1 and 100")]
    public decimal Price { get; set; }
    [Range(minimum: 0, maximum: 1, ErrorMessage = "Quantity in range from 0 to 1")]
    public decimal Discount { get; set; }
    [Display(Name = "Description")]
    public string Description { get; set; }
    [Required]
    [Display(Name = "Category")]
    public string Category { get; set; }
    
}
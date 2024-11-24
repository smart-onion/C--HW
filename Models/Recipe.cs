namespace MVCHW1.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public string? Ingredient { get; set; }
        public string? Category { get; set; }
    }
}

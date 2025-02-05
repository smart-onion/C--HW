using System.ComponentModel.DataAnnotations;

namespace AspIdentityHW1.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

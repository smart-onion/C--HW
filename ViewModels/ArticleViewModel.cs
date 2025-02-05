using System.ComponentModel.DataAnnotations;

namespace AspIdentityHW1.ViewModels
{
    public class ArticleViewModel
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }

    }
}

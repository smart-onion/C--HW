using System.ComponentModel.DataAnnotations;

namespace AspIdentityHW1.ViewModels
{
    public class EditArticleViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}

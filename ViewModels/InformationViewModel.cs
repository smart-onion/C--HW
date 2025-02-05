using AspIdentityHW1.Models;
using System.ComponentModel.DataAnnotations;

namespace AspIdentityHW1.ViewModels
{
    public class InformationViewModel
    {
        public string Email { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}

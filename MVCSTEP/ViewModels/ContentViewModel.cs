using MVCSTEP.Models;

namespace MVCSTEP.ViewModels;

public class ContentViewModel
{
    public IEnumerable<Category> Categories { get; set; }
    public IEnumerable<Publication> Publications { get; set; }
}
using MVCSTEP.Models;
using MVCSTEP.Models.Pages;

namespace MVCSTEP.ViewModels;

public class IndexViewModel
{
    public PagedList<Publication> Publications{ get; set; }
    public List<Category> Categories { get; set; }
}
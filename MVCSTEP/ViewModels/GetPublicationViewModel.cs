using MVCSTEP.Models;

namespace MVCSTEP.ViewModels;

public class GetPublicationViewModel
{
    public Publication Publication { get; set; }
    public string? ReturnUrl { get; set; }
}
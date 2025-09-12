using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Interfaces;

namespace MVCSTEP.ViewModels;

public class RelatedPublicationsViewComponent : ViewComponent
{
    private readonly IPublication _publications;

    public RelatedPublicationsViewComponent(IPublication publications)
    {
        _publications = publications;
    }

    public async Task<IViewComponentResult> InvokeAsync(string id)
    {
        return View("RelatedPublications", await _publications.GetFourRandomPublicationsAsync(id));
    }
}
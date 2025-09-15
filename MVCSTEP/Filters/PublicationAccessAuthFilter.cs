using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVCSTEP.Data;

namespace MVCSTEP.Filters;

public class PublicationAccessAuthFilter : Attribute, IAuthorizationFilter
{
    private string _policyName = "PublicationAccess";

    public PublicationAccessAuthFilter(string policyName)
    {
        _policyName = policyName;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var applicationContext = context.HttpContext.RequestServices.GetService<ApplicationContext>();
        var authorizationService = context.HttpContext.RequestServices.GetService<IAuthorizationService>();
        if (authorizationService == null || applicationContext == null) return;
        
        int.TryParse(context.RouteData.Values["id"]?.ToString(), out int pubId);
        if (pubId == null) context.Result = new UnauthorizedObjectResult("Note id not provided.");

        var publication = applicationContext.Publication.Find(pubId);
        var authResult =
            authorizationService.AuthorizeAsync(context.HttpContext.User, publication, _policyName).Result;
        if (!authResult.Succeeded)
        {
            context.Result = new ForbidResult();
        }
    }
}
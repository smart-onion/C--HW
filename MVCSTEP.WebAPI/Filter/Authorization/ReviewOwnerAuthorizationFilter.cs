using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVCSTEP.Infrastructure.Database_Context;

namespace MVCSTEP.WebAPI.Filter.Authorization;

public class ReviewOwnerAuthorizationFilter : Attribute, IAuthorizationFilter
{
    private readonly ApplicationContext _applicationContext;
    private readonly IAuthorizationService _authorization;

    public ReviewOwnerAuthorizationFilter(ApplicationContext applicationContext, IAuthorizationService authorization)
    {
        _applicationContext = applicationContext;
        _authorization = authorization;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        int.TryParse(context.RouteData.Values["id"]?.ToString(), out int reviewId);
        if (reviewId == null) context.Result = new UnauthorizedObjectResult("Note id not provided.");

        var review = _applicationContext.Products.Find(reviewId);
        var authResult =
            _authorization.AuthorizeAsync(context.HttpContext.User, review, "ReviewOwner").Result;
        if (!authResult.Succeeded)
        {
            context.Result = new ForbidResult();
        }
    }
}
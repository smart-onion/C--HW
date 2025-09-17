using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVCSTEP.Infrastructure.Database_Context;

namespace MVCSTEP.WebAPI.Filter.Authorization;

public class ProductOwnerAuthorizationFilter : Attribute,IAuthorizationFilter
{
    private readonly ApplicationContext _applicationContext;
    private readonly IAuthorizationService _authorization;

    public ProductOwnerAuthorizationFilter(ApplicationContext applicationContext, IAuthorizationService authorization)
    {
        _applicationContext = applicationContext;
        _authorization = authorization;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        int.TryParse(context.RouteData.Values["id"]?.ToString(), out int productId);
        if (productId == null) context.Result = new UnauthorizedObjectResult("Note id not provided.");

        var product = _applicationContext.Products.Find(productId);
        var authResult =
            _authorization.AuthorizeAsync(context.HttpContext.User, product, "ProductOwner").Result;
        if (!authResult.Succeeded)
        {
            context.Result = new ForbidResult();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVCSTEP.Data;

namespace MVCSTEP.Filters;

public class NoteOwnerAuthFilter: Attribute, IAsyncAuthorizationFilter
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ApplicationContext _context;
    public NoteOwnerAuthFilter(IAuthorizationService authorizationService, ApplicationContext context)
    {
        _authorizationService = authorizationService;
        _context = context;
    }


    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        int.TryParse(context.RouteData.Values["id"]?.ToString(), out int noteId);
        if (noteId == null) context.Result = new UnauthorizedObjectResult("Note id not provided.");
        var note = await _context.Note.FindAsync(noteId);
        var authResult = await _authorizationService.AuthorizeAsync(context.HttpContext.User, note, "NoteOwner");
        if (!authResult.Succeeded)
        {
            context.Result = new ForbidResult();
        }
    }
}
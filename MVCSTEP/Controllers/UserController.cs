using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVCSTEP.Models;

namespace MVCSTEP.Controllers;

public class UserController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult RegisterSuccess(User user)
    {
        return View(user);
    }
    [HttpPost]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction(nameof(RegisterSuccess), user);
        }
        
        var errors = new List<string>();
        foreach (var value in ModelState.Values)
        {
            foreach (var error in value.Errors)
                {
                errors.Add(error.ErrorMessage);
                }
        }
        ViewData["errors"] = errors;
        return View(user);
    }
}
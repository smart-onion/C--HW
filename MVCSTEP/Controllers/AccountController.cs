using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Filters;
using MVCSTEP.Helpers;
using MVCSTEP.Models;
using MVCSTEP.ViewModels;

namespace MVCSTEP.Controllers;
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var existUser = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
        if (existUser != null)
        {
            ModelState.AddModelError(string.Empty, "Email already registered.");
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            return View(model);
        }

        var user = new User { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password!);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Login));
        }

        ModelState.AddModelError(string.Empty, "Some Error occured.");

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl)) return Redirect(model.ReturnUrl);
            return RedirectToAction("Index", "Note");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> EditUserInfo()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if  (user == null) return NotFound();
        return View(new EditUserInfofViewModel() {Email = user.Email!});
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditUserInfo(EditUserInfofViewModel model)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if  (user == null) return NotFound();
        user.Email = model.Email;
        user.UserName = model.Email;
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded) return RedirectToAction("Index", "Home");
        ModelState.AddModelError(string.Empty, "Some Error occured.");
        return View(model);
    }

    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null) return NotFound();
        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword!);
        if (result.Succeeded) return RedirectToAction("Index", "Home");
        ModelState.AddModelError(string.Empty, "Some Error occured.");
        return View(model);
    }
}
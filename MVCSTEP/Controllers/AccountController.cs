using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MVCSTEP.Filters;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;
using MVCSTEP.Services;
using MVCSTEP.ViewModels;

namespace MVCSTEP.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IEmailSender _emailSender;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
        IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
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
        await _userManager.AddToRoleAsync(user, "User");
        if (result.Succeeded)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmUrl = Url.Action(nameof(ConfirmEmail), "Account", new { email = user.Email, token },
                Request.Scheme);

            var sendResult = await _emailSender.SendEmailWithTokenAsync(user.Email, confirmUrl);

            if (!sendResult)
            {
                ModelState.AddModelError(string.Empty, "Email could not be confirmed.");
                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }

        ModelState.AddModelError(string.Empty, "Some Error occured.");

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return RedirectToAction(nameof(Error));
        var result = await _userManager.ConfirmEmailAsync(user, token);
        return View(result.Succeeded ? nameof(SuccessRegistration) : nameof(Error));
    }

    [HttpGet]
    public IActionResult SuccessRegistration()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Error()
    {
        return View();
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
        if (user == null) return NotFound();
        return View(new EditUserInfofViewModel() { Email = user.Email! });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditUserInfo(EditUserInfofViewModel model)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null) return NotFound();
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

    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return NotFound();
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetUrl = Url.Action(nameof(ResetPasswordConfirm), "Account", new { email = email, token }, Request.Scheme);
        
        var sendResult = await _emailSender.SendEmailWithTokenAsync(user.Email, resetUrl);
        if (!sendResult)
        {
            TempData["ResetPasswordError"] = "Password could not be reset.";
        }
        else
        {
            TempData["ResetPasswordSuccess"] = "Email with link sent to email.";
        }
        return View();
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirm(string  token, string email)
    {
        var resetViewModel = new ResetPasswordViewModel { Token = token, Email = email };
        return View(resetViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPasswordConfirm(ResetPasswordViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return NotFound();
        
        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        if (result.Succeeded) return RedirectToAction(nameof(Login));
        ModelState.AddModelError(string.Empty, "Some Error occured.");
        return View(model);
    }
}
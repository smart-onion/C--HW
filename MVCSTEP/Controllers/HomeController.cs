using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Models;
using MVCSTEP.Repositories;

namespace MVCSTEP.Controllers;

public class HomeController : Controller
{
    private readonly UserRepository userRepository;
 
    public HomeController(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Login()
    {
        if (User.Identity!.IsAuthenticated)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return View();
        }
    }
    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        if (ModelState.IsValid)
        {
            var currentUser = userRepository.Users.FirstOrDefault(e => e.Email.Equals(user.Email));
            if (currentUser != null)
            {
                if (currentUser.Password.Equals(user.Password))
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, currentUser.Email),
                        new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, currentUser.Role.Name),
                    new Claim(ClaimTypes.DateOfBirth, currentUser.CreatedAt.ToString())};
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction(nameof(Admin));
                }
                else
                {
                    ModelState.AddModelError("Password", "Wrong password.");
                }
            }
            ModelState.AddModelError("Email", "Can`t find this email addres!");
            return View(user);
        }
        else
        {
            return View(user);
        }
    }
    [Authorize(Roles = "Admin,Editor", Policy = "OnlyForAdults")]
    public IActionResult Admin()
    {
        return View();
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Index));
    }
}
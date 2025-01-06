using AuthenticationHW.Data;
using AuthenticationHW.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AuthenticationHW.ViewModel;

namespace AuthenticationHW.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext context;

        public AccountController(ApplicationContext context)
        {
            this.context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel account)
        {
            if(ModelState.IsValid)
            {

                var user = await context.Accounts
                    .FirstOrDefaultAsync(u => u.EmailAddress == account.EmailAddress && u.Password == account.Password);

                if(user == null)
                {
                    Response.StatusCode = StatusCodes.Status401Unauthorized;

                    ModelState.AddModelError("", "Email or password incorrect");
                    
                    return View(account);
                }
                

                var claims = new List<Claim> 
                { 
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.EmailAddress)
                };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                return RedirectToAction("Index", "Home");
            }

            return View(account);
        }
        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Account account)
        {
            if(ModelState.IsValid)
            {
                var user = await context.Accounts.FirstOrDefaultAsync(u => u.EmailAddress.Equals(account.EmailAddress));
                if(user != null)
                {
                    ModelState.AddModelError("", "Email exist!");
                    return View(account);
                }

                context.Accounts.Add(account);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Login));
            }
            return View(account);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}

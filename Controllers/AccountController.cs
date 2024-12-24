using hw10.Data;
using hw10.Models;
using hw10.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hw10.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext context;
        public AccountController(ApplicationContext context) 
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await  context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Username = register.Username,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    Phone = register.Phone,
                    CreditCard = register.CreditCard,
                    Age = register.Age,
                    Password = register.Password,
                    Website = register.Website,
                };
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return Content("true");
            }

            return View(register);
        }
    }
}

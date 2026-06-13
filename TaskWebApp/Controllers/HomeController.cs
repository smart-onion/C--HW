using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskWebApp.Data;

namespace TaskWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var users = await _db.Users
                .OrderByDescending(u => u.LastLoginDate)
                .ToListAsync();
            return View(users);
        }

        public IActionResult About()   => View();
        public IActionResult Contact() => View();
    }
}
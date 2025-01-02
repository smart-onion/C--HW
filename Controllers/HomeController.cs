using hw11.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hw11.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Theme = Request.Cookies["theme"] ?? ThemeColor.White.ToString();
            return View();
        }

        [HttpPost]
        public IActionResult ChangeTheme(ThemeColor theme)
        {
            Response.Cookies.Append("theme", theme.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}

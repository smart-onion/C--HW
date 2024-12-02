using hw6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hw6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetNames()
        {
            return Json(new { names = new[] { "Lex", "Alex" } });
        }
    }
}

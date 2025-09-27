using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PizzaStar.Controllers;

[Authorize(Roles = "Admin,Editor")]
public class PanelController : Controller
{
    [Route("/panel/index")]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [Authorize(Roles = "Admin")]
    [Route("/panel/users")]
    [HttpGet]
    public IActionResult Users()
    {
        return View();
    }
    [Route("/panel/categories")]
    [HttpGet]
    public IActionResult Categories()
    {
        return View();
    }
    [Route("/panel/dishes")]
    [HttpGet]
    public IActionResult Dishes()
    {
        return View();
    }
    [Authorize(Roles = "Admin")]
    [Route("/panel/statistics")]
    [HttpGet]
    public IActionResult Statistics()
    {
        return View();
    }
}
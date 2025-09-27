using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaStar.Models;

namespace PizzaStar.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Models;

namespace MVCSTEP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    
    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    [Authorize]
    public IActionResult Privacy()
    {
        return Content("Privacy");
    }
}
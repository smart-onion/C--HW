using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Models;

namespace MVCSTEP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new Product());
    }
    [HttpPost]
    public IActionResult Index(Product product)
    {
        if (!ModelState.IsValid)
        {
            return Json(product);
        }
        return View(product);
    }
   

}
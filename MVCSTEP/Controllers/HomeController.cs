using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Models;
using MVCSTEP.Services;

namespace MVCSTEP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EmailSenderService  _emailSenderService;
    public HomeController(ILogger<HomeController> logger, EmailSenderService  emailSenderService)
    {
        _logger = logger;
        _emailSenderService = emailSenderService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendEmail(Message message)
    {
        _emailSenderService.SendMessageAsync(message);
        return Content("ok");
    }
}
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Models;

namespace MVCSTEP.Controllers;

public class HomeController : Controller
{
   public IActionResult Index()
   {
      return View();
   }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Models;
using MVCSTEP.ViewModels;

namespace MVCSTEP.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var model = users.Select(u => new UserViewModel
        {
            Id = u.Id,
            Name = u.UserName,
            Email = u.Email,
            Roles = _userManager.GetRolesAsync(u).Result
        }).ToList();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        ViewBag.Roles = GetRoles();
        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault();
        return View(new EditUserViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Role = role
        });
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(EditUserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null) return NotFound();

        user.Email = model.Email;
        user.UserName = model.Email;
        await _userManager.AddToRoleAsync(user, model.Role);

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded) return RedirectToAction("Index");
        ModelState.AddModelError(string.Empty, "Some Error occured.");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();
        await _userManager.DeleteAsync(user);
        return RedirectToAction("Index", "Home");
    }

   
    private IEnumerable<string?> GetRoles() => _roleManager.Roles.Select(r => r.Name).ToList();
}
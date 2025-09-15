using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;
using MVCSTEP.Repositories;
using MVCSTEP.ViewModels;

namespace MVCSTEP.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IFriend _friendRepository;

    public ProfileController(UserManager<User> userManager, IFriend friendRepository)
    {
        _userManager = userManager;
        _friendRepository = friendRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile(string? id)
    {
        User? user;
        if (string.IsNullOrEmpty(id))
        {
            user = await _userManager.GetUserAsync(User);
        }
        else
        {
            user = await _userManager.FindByIdAsync(id);
        }

        if (user == null) return NotFound();

        var friends = await _friendRepository.GetFriendsAsync(user.Id);


        var model = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.UserName,
            Friends = friends
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddToFriend(string id)
    {
        var user = await _userManager.GetUserAsync(User);
        await _friendRepository.SendFriendRequestAsync(user.Id, id);
        return RedirectToAction(nameof(GetProfile), new {id});
    }

    [HttpGet]
    public async Task<IActionResult> AcceptFriendRequest(string id)
    {
        var user = await _userManager.GetUserAsync(User);
        await _friendRepository.AcceptFriendRequestAsync(user.Id, id);
        return RedirectToAction(nameof(GetProfile),new {id});
    }

    [HttpGet]
    public async Task<IActionResult> AllFriendRequests()
    {
        var user = await _userManager.GetUserAsync(User);
        var friends = await _friendRepository.GetRequestsAsync(user.Id);
        var model = friends.Select(u => new UserViewModel
        {
            Id = u.Id,
            Email = u.Email,
            Name = u.UserName,
        });
        return View(model);
    }
}
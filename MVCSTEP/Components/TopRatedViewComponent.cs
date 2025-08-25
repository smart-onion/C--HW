using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Models;

namespace MVCSTEP.Components;

public class TopRatedViewComponent:ViewComponent
{
    private List<User> _topRatedUsers;

    public TopRatedViewComponent()
    {
        _topRatedUsers = new List<User>()
        {
            new User() {Name = "Alex", Age = 18, Score = 821},
            new User() {Name = "Alex", Age = 18, Score = 741},
            new User() {Name = "Alex", Age = 18,  Score = 1002},
        };
    }
    
    public IViewComponentResult Invoke()
    {
        var list = _topRatedUsers.OrderByDescending(u => u.Score).Take(10).ToList();
        return View(list);
    }
}
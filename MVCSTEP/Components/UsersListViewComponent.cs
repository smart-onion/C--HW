using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace MVCSTEP.Components;

public class UsersListViewComponent : ViewComponent
{
    List<string> users;

    public UsersListViewComponent()
    {
        users = new List<string>
        {
            "Tom", "Tim", "Bob", "Sam"
        };
    }

    public IViewComponentResult Invoke()
    {
        int number = users.Count;
        // если передан параметр number
        if (Request.Query.ContainsKey("number"))
        {
            Int32.TryParse(Request.Query["number"].ToString(), out number);
        }
 
        ViewBag.Users = users.Take(number);
        ViewData["Header"] = $"Users count: {number}.";
        return View();
    }
}
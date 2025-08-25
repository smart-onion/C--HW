using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Models;

namespace MVCSTEP.Components;

public class PersonViewComponent: ViewComponent
{
    public string Invoke(User user)
    {
        return $"Name: {user.Name}, Age: {user.Age}";
    }
}
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Services;

namespace MVCSTEP.Components;

public class TimeViewComponent: ViewComponent
{
    private readonly ITimeService _timeService;

    public TimeViewComponent(ITimeService timeService)
    {
        _timeService = timeService;
    }

    public IViewComponentResult Invoke()
    {
        return Content(_timeService.GetTime());
    }
}
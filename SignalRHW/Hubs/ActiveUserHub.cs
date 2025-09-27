using Microsoft.AspNetCore.SignalR;
using SignalRHW.Services;

namespace SignalRHW.Hubs;

public class ActiveUserHub: Hub
{
    private readonly ActiveUsersService  _activeUsersService;

    public ActiveUserHub(ActiveUsersService activeUsersService)
    {
        _activeUsersService = activeUsersService;
    }
    
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("Connected" + Context.ConnectionId);
        _activeUsersService.AddUser(Context.ConnectionId);
        await Clients.All.SendAsync("UserCount", _activeUsersService.GetUserCount());
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        _activeUsersService.RemoveUser(Context.ConnectionId);
        await Clients.All.SendAsync("UserCount", _activeUsersService.GetUserCount());
        await base.OnConnectedAsync();
    }
}
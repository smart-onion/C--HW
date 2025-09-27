using Microsoft.AspNetCore.SignalR;

namespace SignalrMVC.Hubs;

public class DrawingHub : Hub
{
    public async Task SendDrawData(int x1, int y1, int x2, int y2)
    {
        await Clients.Others.SendAsync("ReceiveDrawData", x1, y1, x2, y2);
    }
}
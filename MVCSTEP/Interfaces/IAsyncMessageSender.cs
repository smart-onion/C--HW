using MVCSTEP.Models;

namespace MVCSTEP.Interfaces;

public interface IAsyncMessageSender
{
    public Task SendMessageAsync(Message message);
}
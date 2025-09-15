using MVCSTEP.Interfaces;

namespace MVCSTEP.Services;

public class ConsoleEmailSenderService : IEmailSender
{
    public Task<bool> SendEmailWithTokenAsync(string userEmail, string link)
    {
        Console.WriteLine(link);
        return Task.FromResult(true);
    }
}
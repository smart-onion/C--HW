namespace MVCSTEP.Interfaces;

public interface IEmailSender
{
    public Task<bool> SendEmailWithTokenAsync(string userEmail, string link);
}
using MailKit.Net.Smtp;
using MimeKit;

namespace MVCSTEP.Helpers;

public class EmailHelper
{
    public async Task<bool> SendEmailRegistrationConfirm(string userEmail, string link)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("WebApplication1 Project", "enykoruna1@gmail.com"));
        message.To.Add(new MailboxAddress(name: userEmail, address: userEmail));
        message.Subject = "Confirmation of registration on the WebApplication1 website";
        message.Body = new TextPart("html")
        {
            Text = link,
        };
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("@gmail.com", "");
 
            try
            {
                await client.SendAsync(message);
                return true;
            }
            catch (Exception)
            {
                //Logging information
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
        return false;
    }
}
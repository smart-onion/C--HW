using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MVCSTEP.Interfaces;

namespace MVCSTEP.Services;

public class EmailSenderService: IEmailSender
{
    public async Task<bool> SendEmailWithTokenAsync(string userEmail, string link)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("HW Identity", "sanyamart13@gmail.com"));
        message.To.Add(new MailboxAddress(name: userEmail, address: userEmail));
        message.Subject = "Confirmation of registration on the HW Identity website";
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
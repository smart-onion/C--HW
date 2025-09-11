using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Services;

public class EmailSenderService: IAsyncMessageSender
{
    private readonly EmailSettings _emailConfig;

    public EmailSenderService(EmailSettings emailConfig)
    {
        _emailConfig = emailConfig;
    }
    public Task SendMessageAsync(Message message)
    {
        var mail = CreateEmailMessage(message);
        Send(mail);
        return Task.CompletedTask;
    }
    
    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
        emailMessage.To.Add(new MailboxAddress(message.To, message.To));
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Body };
        emailMessage.Date = DateTime.Now.Add(message.Delay ?? TimeSpan.Zero);
        return emailMessage;
    }
    
    private void Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                //client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                client.Send(mailMessage);
            }
            catch
            {
  
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }

}
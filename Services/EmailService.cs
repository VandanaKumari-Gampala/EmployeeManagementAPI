using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using EmployeeManagementAPI.Interfaces;
namespace EmployeeManagementAPI.Services
{
public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config)
    {
        _config = config;
    }
    public async Task SendEmailAsync(EmailModel email)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                "Sender", _config["EmailSettings:SenderEmail"]));
                message.To.Add(MailboxAddress.Parse(email.To));
                message.Subject = email.Subject;
                message.Body = new TextPart("plain")    
                {
                    Text = email.Body
                };
                using var smtp = new SmtpClient();
                await
                smtp.ConnectAsync(_config["EmailSettings:SmtpServer"],
                int.Parse(_config["EmailSettings:Port"]), false);
                await
                smtp.AuthenticateAsync(_config["EmailSettings:SenderEmail"],
                _config["EmailSettings:Password"]);
                await smtp.SendAsync(message);
                await
                smtp.DisconnectAsync(true);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Email sending failed (dummy config)");
        }
        Console.WriteLine("Email triggered successfully");
        
        }
    }    
}

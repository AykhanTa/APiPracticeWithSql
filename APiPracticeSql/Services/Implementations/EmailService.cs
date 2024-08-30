using System.Net.Mail;
using System.Net;
using APiPracticeSql.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace APiPracticeSql.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string email, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("aykhanft@code.edu.az", "ShopApp");
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;


            mailMessage.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("aykhanft@code.edu.az", "mbzq vnqx rjjb chkm");

            smtpClient.Send(mailMessage);
        }
    }
}

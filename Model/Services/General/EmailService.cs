using System.Net.Mail;
using System.Net;
using Model.Services.Interfaces;

namespace Model.Services.General
{
    public class EmailService : IEmailService
    {
        public void SendVerficationEmail(string userEmail, Guid key)
        {
            var fromAddress = "progamingpartswebsite@gmail.com";
            var fromPassword = "ebsq yrnw mmnv jika";

            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress, fromPassword)
            };

            using var message = new MailMessage(fromAddress, "wiktorzme@gmail.com");
            message.Subject = "ProGamingParts - Weryfikacja konta.";
            message.Body = "Witaj, w celu aktywacji swojego konta, wejdz w ten link: " +
                           $"https://localhost:5001/AccountVerification/?userEmail={userEmail}&key={key}";
            smtpClient.Send(message);
        }
    }
}

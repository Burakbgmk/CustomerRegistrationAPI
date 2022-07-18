using CustomerRegistration.Report.Services.Abstractions;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;

namespace CustomerRegistration.Report.Services
{
    public class EmailSenderService : IEmailSenderService
    {

        public async Task EmailSend(string receiver)
        {
            var sender = new SmtpSender(() => new SmtpClient("smtp.outlook.com")
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "",//Email to send from
                    Password = ""//Email password
                }
            });

            Email.DefaultSender = sender;

            var email = await Email
                .From("")//Email to send from
                .To(receiver)
                .Subject("Deneme123")
                .Body("Thanks for sldfksdf")
                .AttachFromFilename("D:/Desktop/Patika/LINK-BOOTCAMP/BitirmeProjesi/ExcelFile/Demo.xlsx", "application/vnd.ms-excel", "Demo.xlsx")
                .SendAsync();
        }
    }
}

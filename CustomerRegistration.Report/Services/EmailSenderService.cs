using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;

namespace CustomerRegistration.Report.Services
{
    public class EmailSenderService
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
                    UserName = "Burak_029920@hotmail.com",
                    Password = "Thewill1685"
                }
            });

            Email.DefaultSender = sender;

            var email = await Email
                .From("Burak_029920@hotmail.com")
                .To(receiver)
                .Subject("Deneme123")
                .Body("Thanks for sldfksdf")
                .AttachFromFilename("D:/Desktop/Patika/LINK-BOOTCAMP/BitirmeProjesi/ExcelFile/Demo.xlsx", "application/vnd.ms-excel", "Demo.xlsx")
                .SendAsync();
        }
    }
}

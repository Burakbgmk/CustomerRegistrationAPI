using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Report.Services.Abstractions
{
    public interface IEmailSenderService
    {
        Task EmailSend(string receiver);
    }
}

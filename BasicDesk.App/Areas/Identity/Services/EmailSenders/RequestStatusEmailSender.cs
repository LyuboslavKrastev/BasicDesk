using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace BasicDesk.App.Areas.Identity.Services
{
    public class RequestStatusEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.Run(() => Console.WriteLine());
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BasicDesk.App.Areas.Identity.Services
{
    public class VerificationEmailSender : IEmailSender
    {
        private EmailSenderOptions options;

        public VerificationEmailSender(IOptions<EmailSenderOptions> options)
        {
            this.options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = this.options.SendGridApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("krastevlyuboslav@gmail.com", "BasicDesk");
            var to = new EmailAddress(email, email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
            var response = await client.SendEmailAsync(msg);
            var statusCode = response.StatusCode;
            var body = await response.Body.ReadAsStringAsync();
        }
    }
}

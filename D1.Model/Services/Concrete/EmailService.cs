using System.Threading.Tasks;
using D1.Model.Services.Abstract;
using MailKit.Net.Smtp;
using MimeKit;

namespace D1.Model.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private ISettingsService _settingsService;

        public EmailService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task SendEmailAsync(string email, string subject, string messageText)
        {

            var message = new MimeMessage();
            
            message.From.Add(new MailboxAddress(_settingsService.EmailSettings.FromName, _settingsService.EmailSettings.FromEmail));
            message.To.Add(new MailboxAddress("", "stepnaturesto@gmail.com"));
            message.Subject = subject;


            message.Body = new TextPart("plain")
            {
                Text =messageText
            };


            
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_settingsService.EmailSettings.Host, _settingsService.EmailSettings.Port, _settingsService.EmailSettings.UseSsl);
                await client.AuthenticateAsync(_settingsService.EmailSettings.UsernameEmail, _settingsService.EmailSettings.UsernamePassword);
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}

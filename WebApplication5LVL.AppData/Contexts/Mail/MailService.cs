using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace WebApplication5LVL.AppData.Contexts.Mail
{
    public sealed class MailService : IMailService
    {
        public IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> SendMessage(string message, string email, CancellationToken cancellation)
        {
            var subject = "Congratulation mail";
            try
            {
                using var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Solar", configuration["Mail:Address"]));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.mail.ru", 587, false);
                    await client.AuthenticateAsync(configuration["Mail:Address"], configuration["Mail:Password"]);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
                return "Сообщение успешно отправлено";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SocialNetwork.Core.Application.DTO_S.Email;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Domain.Settings;


namespace SocialNetwork.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private MailSettings _mailSettings { get; }

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest request)
        {
            try
            {
                MimeMessage email = new();
                email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                BodyBuilder builder = new();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();
                using SmtpClient smtp = new();
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

            }catch(Exception e)
            {

            }
        }
    }
}

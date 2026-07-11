using BusinessLogic.Configurations;
using BusinessLogic.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailOptions> emailOptions, ILogger<EmailService> logger)
        {
            _emailOptions = emailOptions.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlContent)
        {

            var fromAddress = _emailOptions.FromAddress;
            var smtpHost = _emailOptions.SmtpHost;
            var smtpUser = _emailOptions.SmtpUser;
            var smtpPassword = _emailOptions.SmtpPassword;
            var enableSsl = _emailOptions.EnableSsl;
            var smtpPort = _emailOptions.SmtpPort;


            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPassword),
                EnableSsl = enableSsl
            };

            var message = new MailMessage(fromAddress, to, subject, htmlContent)
            {
                IsBodyHtml = true
            };

            try
            {
                await client.SendMailAsync(message);
                _logger.LogInformation("Email sent to {Email}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", to);
                throw;
            }
        }
    }
}

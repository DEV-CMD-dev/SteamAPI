using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Order;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
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

        public async Task SendConfirmationLink(string to, string link, int expirationTime)
        {
            var template = await File.ReadAllTextAsync("EmailTemplates/ConfirmEmail.html");
            var subject = "Email confirmation";

            template = template
                .Replace("{{Link}}", link)
                .Replace("{{ExpirationTime}}", expirationTime.ToString());

            await SendEmailAsync(to, subject, template);
        }

        public async Task SendPasswordResetLink(string to, string link, int expirationTime)
        {
            var template = await File.ReadAllTextAsync("EmailTemplates/PasswordReset.html");

            template = template
                .Replace("{{Link}}", link)
                .Replace("{{ExpirationTime}}", expirationTime.ToString());

            await SendEmailAsync(to, "Password Reset", template);
        }


        public async Task SendTwoFactorCode(string to, string code)
        {
            var template = await File.ReadAllTextAsync("EmailTemplates/TwoFactorCode.html");

            template = template.Replace("{{Code}}", code);

            await SendEmailAsync(to, "2FA Code", template);
        }

        public async Task SendOrderReceiptAsync(string to, string userName, Order order, List<OrderItemDto> items)
        {
            var template = await File.ReadAllTextAsync("EmailTemplates/OrderReceipt.html");       
            var itemsHtml = string.Join("", items.Select(i => $@"
            <tr>
                <td style='padding:15px 0; color:#bdbdbd; border-top:1px solid rgba(255,255,255,0.05);'>{i.Title}</td>
                <td style='padding:15px 0; color:#ffffff; font-weight:bold; text-align:right; border-top:1px solid rgba(255,255,255,0.05);'>{i.Price:0.00} $    </td>
            </tr>"));

            template = template
                .Replace("{{UserName}}", userName)
                .Replace("{{UserEmail}}", to) 
                .Replace("{{OrderId}}", order.Id.ToString())
                .Replace("{{OrderDate}}", order.OrderDate.ToString("dd MMMM yyyy")) 
                .Replace("{{TotalAmount}}", order.TotalAmount.ToString("0.00"))
                .Replace("{{OrderItems}}", itemsHtml);

            await SendEmailAsync(to, $"Invoice ID: {order.Id} - Thank you for your purchase on Nexus!", template);
        }
    }
}

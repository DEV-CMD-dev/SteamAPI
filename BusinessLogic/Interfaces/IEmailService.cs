namespace BusinessLogic.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string? htmlContent = null);
        Task SendPasswordResetLink(string to, string link, int expirationTime);
        Task SendConfirmationLink(string to, string link, int expirationTime);
        Task SendTwoFactorCode(string to, string code);
    }
}

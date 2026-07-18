using BusinessLogic.DTOs.PasswordReset;
using DataAccess.Data.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IUserHelperService
    {
        Task RequestPasswordResetAsync(RequestPasswordResetTokenDto dto);
        Task ResetPasswordAsync(PasswordResetDto dto);
        Task SendEmailConfirmationAsync(User user);
        Task ConfirmEmailAsync(ConfirmEmailDto dto);
    }
}

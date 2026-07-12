using BusinessLogic.DTOs.PasswordReset;

namespace BusinessLogic.Interfaces
{
    public interface IPasswordService
    {
        Task RequestPasswordResetAsync(RequestPasswordResetTokenDto dto);
        Task ResetPasswordAsync(PasswordResetDto dto);
    }
}

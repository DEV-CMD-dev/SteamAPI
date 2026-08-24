using BusinessLogic.DTOs.Auth;

namespace BusinessLogic.Interfaces
{

    public interface IAuthService
    {
        Task Register(RegisterRequestDto model);
        Task<LoginResponseDto> Login(LoginRequestDto model);
        Task<LoginResponseDto> LoginTwoFactor(LoginTwoFactorRequestDto model);
        Task Logout();
    }
}

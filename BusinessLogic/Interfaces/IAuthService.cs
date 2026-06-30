using BusinessLogic.DTOs.Auth;

namespace BusinessLogic.Interfaces;

public interface IAuthService
{
    Task Register(RegisterRequestDto model);
    Task<LoginResponseDto> Login(LoginRequestDto model);
    Task Logout();
}
using Shared.DTOs.Auth;

namespace Shared.Interfaces;

public interface IAuthService
{
    Task Register(RegisterRequestDto model);
    Task<LoginResponseDto> Login(LoginRequestDto model);
    Task Logout();
}
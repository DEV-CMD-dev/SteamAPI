using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{

    public interface IAuthService
    {
        Task Register(RegisterRequestDto model);
        Task<LoginResponseDto> Login(LoginRequestDto model);
        Task Logout();
    }
}

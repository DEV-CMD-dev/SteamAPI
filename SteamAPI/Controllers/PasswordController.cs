using BusinessLogic.DTOs.PasswordReset;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Сontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestPasswordResetToken(RequestPasswordResetTokenDto dto)
        {
            await _passwordService.RequestPasswordResetAsync(dto);
            return Ok(new
            {
                message = "If the account exists, a password reset email has been sent"
            });
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword(PasswordResetDto dto)
        {
            await _passwordService.ResetPasswordAsync(dto);
            return Ok();
        }
    }
}

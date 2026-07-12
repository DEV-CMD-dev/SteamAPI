using BusinessLogic.DTOs.PasswordReset;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Сontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHelperController : ControllerBase
    {
        private readonly IUserHelperService _userHelperService;

        public UserHelperController(IUserHelperService userHelperService)
        {
            _userHelperService = userHelperService;
        }

        [HttpPost("request-reset-password")]
        public async Task<IActionResult> RequestPasswordResetToken(RequestPasswordResetTokenDto dto)
        {
            await _userHelperService.RequestPasswordResetAsync(dto);
            return Ok(new
            {
                message = "If the account exists, a password reset email has been sent"
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(PasswordResetDto dto)
        {
            await _userHelperService.ResetPasswordAsync(dto);
            return Ok();
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto dto)
        {
            await _userHelperService.ConfirmEmailAsync(dto);
            return Ok();
        }
    }
}

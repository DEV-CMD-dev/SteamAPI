using BusinessLogic.DTOs.PasswordReset;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        
        [Authorize]
        [HttpPost("request-set-2fa")]
        public async Task<IActionResult> RequestSetTwoFactorAuth(RequestSetTwoFactorAuthDto dto)
        {
            var userId = User.GetRequiredUserId();
            await _userHelperService.RequestSetTwoFactorAuthAsync(dto, userId);
            return Ok();
        }
        
        [Authorize]
        [HttpPost("set-2fa")]
        public async Task<IActionResult> SetTwoFactorAuth(SetTwoFactorAuthDto dto)
        {
            var userId = User.GetRequiredUserId();
            await _userHelperService.SetTwoFactorAuthAsync(dto, userId);
            return Ok();
        }
        
        [Authorize]
        [HttpGet("is-2fa-enabled")]
        public async Task<IActionResult> IsTwoFactorAuthEnabled()
        {
            var userId = User.GetRequiredUserId();
            var res = await _userHelperService.IsTwoFactorAuthEnabledAsync(userId);
            return Ok(res);
        }
    }
}

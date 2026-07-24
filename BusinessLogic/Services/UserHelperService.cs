using BusinessLogic.Classes;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.PasswordReset;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net;

namespace BusinessLogic.Services
{
    public class UserHelperService : IUserHelperService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly DataProtectionToken _dataProtectionToken;
        private readonly FrontendOptions _frontendOptions;

        public UserHelperService(
            UserManager<User> userManager,
            IEmailService emailService,
            IOptions<DataProtectionToken> dataProtectionTokenOptions,
            IOptions<FrontendOptions> frontendOptions)
        {
            _userManager = userManager;
            _emailService = emailService;
            _dataProtectionToken = dataProtectionTokenOptions.Value;
            _frontendOptions = frontendOptions.Value;
        }

        public async Task RequestPasswordResetAsync(RequestPasswordResetTokenDto dto)
        {
            var user = dto.Identifier.Contains('@')
                ? await _userManager.FindByEmailAsync(dto.Identifier)
                : await _userManager.FindByNameAsync(dto.Identifier);

            if (user == null)
                return;

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebUtility.UrlEncode(token);
            var encodedEmail = WebUtility.UrlEncode(user.Email);

            var link = $"{_frontendOptions.BaseUrl}/reset-password" +
                $"?identifier={encodedEmail}" +
                $"&token={encodedToken}";
            
            await _emailService.SendEmailAsync(user.Email, "Password Reset", $@"
                <p>Your password reset link:</p>
                <strong>{link}</strong>
                <p>This link will expire in {_dataProtectionToken.ExpirationTimeInMinutes} minutes.</p>
            ");
        }

        public async Task ResetPasswordAsync(PasswordResetDto dto)
        {
            var user = dto.Identifier.Contains('@')
                ? await _userManager.FindByEmailAsync(dto.Identifier)
                : await _userManager.FindByNameAsync(dto.Identifier);

            if (user == null)
                return;
                
            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join("\n", result.Errors.Select(e => e.Description));
                throw new HttpException(errors, HttpStatusCode.BadRequest);
            }
        }

        public async Task SendEmailConfirmationAsync(User user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedToken = WebUtility.UrlEncode(token);
            var encodedEmail = WebUtility.UrlEncode(user.Email);

            var link = $"{_frontendOptions.BaseUrl}/confirm-email" +
                $"?identifier={encodedEmail}" +
                $"&token={encodedToken}";

            await _emailService.SendEmailAsync(user.Email, "Email Confirmation link", $@"
                <p>Your confirmation token:</p>
                <strong>{link}</strong>
                <p>This link will expire in {_dataProtectionToken.ExpirationTimeInMinutes} minutes.</p>
            ");
        }

        public async Task ConfirmEmailAsync(ConfirmEmailDto dto)
        {
            var user = dto.Identifier.Contains('@')
                ? await _userManager.FindByEmailAsync(dto.Identifier)
                : await _userManager.FindByNameAsync(dto.Identifier);

            if (user == null)
                return;

            var result = await _userManager.ConfirmEmailAsync(user, dto.Token);

            if (!result.Succeeded)
            {
                throw new HttpException("Invalid or expired token", HttpStatusCode.BadRequest);
            }
        }

    }
}

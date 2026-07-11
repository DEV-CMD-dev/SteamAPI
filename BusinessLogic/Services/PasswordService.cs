using BusinessLogic.Classes;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.PasswordReset;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;

namespace BusinessLogic.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly PasswordResetOptions _passwordResetOptions;

        public PasswordService(
            UserManager<User> userManager,
            IEmailService emailService,
            IConfiguration configuration,
            IOptions<PasswordResetOptions> passwordResetOptions)
        {
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
            _passwordResetOptions = passwordResetOptions.Value;
        }

        public async Task RequestPasswordResetAsync(RequestPasswordResetTokenDto dto)
        {
            var user = dto.Identifier.Contains('@')
                ? await _userManager.FindByEmailAsync(dto.Identifier)
                : await _userManager.FindByNameAsync(dto.Identifier);

            if (user == null)
                return;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendEmailAsync(user.Email, "Password Reset", $@"
                <p>Your reset password token:</p>
                <strong>{token}</strong>
                <p>This token will expire in {_passwordResetOptions.ExpirationTimeInMinutes} minutes.</p>
            ");
        }

        public async Task ResetPasswordAsync(PasswordResetDto dto)
        {
            var user = dto.Identifier.Contains('@')
                ? await _userManager.FindByEmailAsync(dto.Identifier)
                : await _userManager.FindByNameAsync(dto.Identifier);

            if (user == null)
                return;

            await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);


            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

            if (!result.Succeeded)
            {
                throw new HttpException("Invalid or expired password reset token", HttpStatusCode.BadRequest);
            }
        }

    }
}

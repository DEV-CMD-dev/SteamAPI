using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IUserHelperService _userHelperService;
        private readonly IEmailService _emailService;

        public AuthService(
            UserManager<User> userManager,
            IJwtService jwtService,
            IUserHelperService userHelperService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _userHelperService = userHelperService;
            _emailService = emailService;
        }

        public async Task Register(RegisterRequestDto dto)
        {
            var newUser = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Country = dto.Country,
                UserRole = DataAccess.Enums.UserRole.User,
                UserVisibility = DataAccess.Enums.UserVisibility.Offline,
                CreatedAt = DateTime.UtcNow,
                Profile = new Profile
                {
                    Level = 0,
                    XP = 0
                }
            };

            var result = await _userManager.CreateAsync(newUser, dto.Password);

            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new HttpException($"User registration failed: {errorMessages}", HttpStatusCode.BadRequest);
            }

            await _userHelperService.SendEmailConfirmationAsync(newUser);
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            var user = await _userManager.FindByIdentifierAsync(dto.Identifier);

            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                throw new HttpException("Invalid credentials or email is not confirmed", HttpStatusCode.Unauthorized);

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
                throw new HttpException("Invalid credentials or email is not confirmed", HttpStatusCode.Unauthorized);

            if (user.TwoFactorEnabled)
            {
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultAuthenticatorProvider);
                await _emailService.SendEmailAsync(user.Email, $"Your 2FA Code: {code}");
                return new LoginResponseDto
                {
                    Message = "Two factor code has been sent to your email"
                };
            }

            var claims = _jwtService.GetClaims(user);
            var JWT = _jwtService.GenerateToken(claims);

            return new LoginResponseDto
            {
                AccessToken = JWT.Token,
                ExpirationTime = JWT.ExpirationTime,
                UserName = user.UserName
            };
        }

        public async Task<LoginResponseDto> LoginTwoFactor(LoginTwoFactorRequestDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                throw new HttpException("Invalid credentials or email is not confirmed", HttpStatusCode.Unauthorized);

            var valid = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultAuthenticatorProvider, dto.Code);

            if (!valid)
                throw new HttpException("Invalid 2FA code", HttpStatusCode.BadRequest);

            var claims = _jwtService.GetClaims(user);
            var JWT = _jwtService.GenerateToken(claims);

            return new LoginResponseDto
            {
                AccessToken = JWT.Token,
                ExpirationTime = JWT.ExpirationTime,
                UserName = user.UserName
            };
        }

        public Task Logout()
        {
            return Task.CompletedTask;
        }
    }
}
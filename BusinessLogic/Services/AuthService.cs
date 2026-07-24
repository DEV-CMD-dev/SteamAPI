using BusinessLogic.Classes;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;
using BusinessLogic.Extensions;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IUserHelperService _userHelperService;

        public AuthService(
            UserManager<User> userManager,
            IJwtService jwtService,
            IUserHelperService userHelperService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _userHelperService = userHelperService;
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
                    Level = 1
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

            var claims = _jwtService.GetClaims(user);
            var token = _jwtService.GenerateToken(claims);

            return new LoginResponseDto
            {
                AccessToken = token
            };
        }

        public Task Logout()
        {
            return Task.CompletedTask;
        }
    }
}
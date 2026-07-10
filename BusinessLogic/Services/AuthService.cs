using BusinessLogic.Classes;
using BusinessLogic.DTOs;
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

        public AuthService(UserManager<User> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
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
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            var user = dto.Identifier.Contains('@')
                ? await _userManager.FindByEmailAsync(dto.Identifier)
                : await _userManager.FindByNameAsync(dto.Identifier);

            if (user == null)
                throw new HttpException("Invalid credentials", HttpStatusCode.BadRequest);

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
                throw new HttpException("Invalid credentials", HttpStatusCode.BadRequest);

            var claims = await _jwtService.GetClaimsAsync(user);
            var token = await _jwtService.GenerateTokenAsync(claims);

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
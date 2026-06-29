using Microsoft.AspNetCore.Identity;
using Shared.DTOs.Auth;
using Shared.Entities;
using Shared.Interfaces;

namespace BusinessLogic.Services;

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
            Country = dto.Country
        };
        var result = await _userManager.CreateAsync(newUser, dto.Password);

        if (!result.Succeeded)
        {
            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User registration failed: {errorMessages}");
        }
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto dto)
    {
        var user = dto.Identifier.Contains('@')
            ? await _userManager.FindByEmailAsync(dto.Identifier)
            : await _userManager.FindByNameAsync(dto.Identifier);
        if (user == null) return null;
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!isPasswordValid) return null;

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
namespace BusinessLogic.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string? AccessToken { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public string? UserName { get; set; }
        
        public bool RequireTwoFactorAuth { get; set; }
        public string? Message { get; set; }
    }
}

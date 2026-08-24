namespace BusinessLogic.DTOs.Auth
{
    public class LoginTwoFactorRequestDto
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}

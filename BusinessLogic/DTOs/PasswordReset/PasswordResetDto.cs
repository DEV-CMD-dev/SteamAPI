namespace BusinessLogic.DTOs.PasswordReset
{
    public class PasswordResetDto
    {
        public string Identifier { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}

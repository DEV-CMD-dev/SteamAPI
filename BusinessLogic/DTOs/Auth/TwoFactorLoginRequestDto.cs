namespace BusinessLogic.DTOs.Auth;

public class TwoFactorLoginRequestDto
{
    public string Identifier { get; set; }
    public string Code { get; set; }
}
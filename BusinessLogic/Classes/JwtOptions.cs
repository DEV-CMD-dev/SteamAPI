namespace BusinessLogic.Classes;

public class JwtOptions
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public int LifetimeInMinutes { get; set; }
}
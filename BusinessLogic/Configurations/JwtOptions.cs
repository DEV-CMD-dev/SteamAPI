using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Configurations
{
    public class JwtOptions
    {
        [Required]
        public required string Key { get; set; }
        [Required]
        public required string Issuer { get; set; }
        public int LifetimeInMinutes { get; set; }
    }
}

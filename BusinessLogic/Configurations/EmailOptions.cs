using System.ComponentModel.DataAnnotations;
namespace BusinessLogic.Configurations
{
    public class EmailOptions
    {
        [Required]
        public required string FromAddress { get; set; }
        [Required]
        public required string SmtpHost { get; set; }
        [Required]
        public required string SmtpUser { get; set; }
        [Required]
        public required string SmtpPassword { get; set; }
        public required bool EnableSsl { get; set; }
        public required int SmtpPort { get; set; }
    }
}

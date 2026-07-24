using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Configurations
{
    public class FrontendOptions
    {
        [Required]
        public required string BaseUrl { get; set; }
    }
}

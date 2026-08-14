using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Screenshot
{
    public class CreateScreenshotDto
    {
        public string Url { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Game ID must be greater than zero")]
        public int GameId { get; set; }
    }
}

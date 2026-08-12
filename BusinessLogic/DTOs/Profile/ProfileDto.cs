namespace BusinessLogic.DTOs.Profile
{
    public class ProfileDto
    {
        public string UserId { get; set; }
        public string? Avatar { get; set; }
        public int Level { get; set; }
        public int XP { get; set; }
        public string? Badges { get; set; }
        public string? Showcase { get; set; }
    }
}

namespace BusinessLogic.DTOs.Profile
{
    public class RecentGameDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? CoverImageHorizontal { get; set; }
        public DateTime LastPlayDate { get; set; }
        public int PlayTimeMinutes { get; set; }
        public List<AchievementProgressDto> Achievements { get; set; } = new();
    }

    public class AchievementProgressDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public bool IsUnlocked { get; set; }
    }
}
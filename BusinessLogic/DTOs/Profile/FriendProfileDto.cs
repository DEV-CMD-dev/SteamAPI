namespace BusinessLogic.DTOs.Profile
{
    public class FriendProfileDto
    {
        public string UserId { get; set; }
        public string? Avatar { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsOnline { get; set; }
    }
}

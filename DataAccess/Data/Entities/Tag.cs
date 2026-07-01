namespace DataAccess.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GameTag> GameTags { get; set; } = new();
    }
}

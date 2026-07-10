namespace DataAccess.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Picture { get; set; }
        public List<Game>? Games { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data.Entities
{
    [Index(nameof(Name))]
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Picture { get; set; }
        public List<Game>? Games { get; set; }
    }
}

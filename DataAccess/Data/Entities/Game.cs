using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? DeveloperId { get; set; } 
        public User? Developer { get; set; } 
        public int? PublisherId { get; set; } 
        public User? Publisher { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public List<Tag> Tags { get; set; } = [];
        public string SystemRequirements { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public List<Screenshot> Screenshots { get; set; } = [];
        public List<GameVersion> Versions { get; set; } = [];
        public List<Achievement> Achievements { get; set; } = [];

    }
}

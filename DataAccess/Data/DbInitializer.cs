using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public static class DbInitializer
    {
        public static void SeedSteamData(this ModelBuilder modelBuilder)
        {
          
     
            modelBuilder.Entity<Tag>().HasData(new List<Tag>()
            {
                new() { Id = 1, TagTitle = "Action" },
                new() { Id = 2, TagTitle = "Co-op" },
                new() { Id = 3, TagTitle = "Cyberpunk" },
                new() { Id = 4, TagTitle = "Souls-like" }
            });

         
            modelBuilder.Entity<Game>().HasData(new List<Game>()
            {
                new()
                {
                    Id = 1,
                    Title = "Counter-Strike 2",
                    Description = "Tactical shooter.",
                  
                    ReleaseDate = new DateOnly(2023, 9, 27),
                    Price = 0.00m,
                    SystemRequirements = "Windows 10, 8GB RAM",
                    CoverImage = "https://example.com/covers/cs2.jpg"
                },
                new()
                {
                    Id = 2,
                    Title = "Neon City 2026",
                    Description = "Cyberpunk RPG.",
              
                    ReleaseDate = new DateOnly(2026, 5, 12),
                    Price = 29.99m,
                    SystemRequirements = "Windows 11, 16GB RAM",
                    CoverImage = "https://example.com/covers/neon.jpg"
                },
                new()
                {
                    Id = 3,
                    Title = "Elden Ring",
                    Description = "Rise, Tarnished.",
            
                    ReleaseDate = new DateOnly(2022, 2, 25),
                    Price = 59.99m,
                    SystemRequirements = "Windows 10, RTX 2060",
                    CoverImage = "https://example.com/covers/elden.jpg"
                }
            });
            modelBuilder.Entity<Achievement>().HasData(new List<Achievement>()
            {
                new() { Id = 1, Name = "First Blood", GameId = 1, IconUrl = "https://example.com/icons/first_blood.png" },
                new() { Id = 2, Name = "Master Hacker", GameId = 2, IconUrl = "https://example.com/icons/hacker.png" },
                new() { Id = 3, Name = "Lord of Frenzied Flame", GameId = 3, IconUrl = "https://example.com/icons/elden_lord.png" }
            });
        }
    }
}
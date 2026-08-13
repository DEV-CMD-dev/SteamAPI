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
                new() { Id = 1, Name = "Action" },
                new() { Id = 2, Name = "Co-op" },
                new() { Id = 3, Name = "Cyberpunk" },
                new() { Id = 4, Name = "Souls-like" }
            });

          
            modelBuilder.Entity<Game>().HasData(new List<Game>()
            {
                new()
                {
                    Id = 1,
                    Title = "Counter-Strike 2",
                    Description = "Tactical shooter.",
                    ReleaseDate = new DateTime(2023, 9, 27),
                    Price = 0.00m,
                    SystemRequirements = "Windows 10, 8GB RAM"
                },
                new()
                {
                    Id = 2,
                    Title = "Neon City 2026",
                    Description = "Cyberpunk RPG.",
                    ReleaseDate = new DateTime(2026, 5, 12),
                    Price = 29.99m,
                    SystemRequirements = "Windows 11, 16GB RAM"
                },
                new()
                {
                    Id = 3,
                    Title = "Elden Ring",
                    Description = "Rise, Tarnished.",
                    ReleaseDate = new DateTime(2022, 2, 25),
                    Price = 59.99m,
                    SystemRequirements = "Windows 10, RTX 2060"
                }
            });

            modelBuilder.Entity("GameTag").HasData(
                new { GamesId = 1, TagsId = 1 }, // CS2 -> Action
                new { GamesId = 1, TagsId = 2 }, // CS2 -> Co-op
                new { GamesId = 2, TagsId = 1 }, // Neon City -> Action
                new { GamesId = 2, TagsId = 3 }, // Neon City -> Cyberpunk
                new { GamesId = 3, TagsId = 1 }, // Elden Ring -> Action
                new { GamesId = 3, TagsId = 4 }  // Elden Ring -> Souls-like
            );

            // 4. Сід досягнень
            modelBuilder.Entity<Achievement>().HasData(new List<Achievement>()
            {
                new() { Id = 1, Name = "First Blood", GameId = 1, IconUrl = "https://example.com/icons/first_blood.png" },
                new() { Id = 2, Name = "Master Hacker", GameId = 2, IconUrl = "https://example.com/icons/hacker.png" },
                new() { Id = 3, Name = "Lord of Frenzied Flame", GameId = 3, IconUrl = "https://example.com/icons/elden_lord.png" }
            });

            // 5. Сід скриншотів
            modelBuilder.Entity<Screenshot>().HasData(new List<Screenshot>()
            {
                new() { Id = 1, GameId = 1, Url = "https://example.com/screenshots/cs2_1.jpg" },
                new() { Id = 2, GameId = 1, Url = "https://example.com/screenshots/cs2_2.jpg" },
                new() { Id = 3, GameId = 2, Url = "https://example.com/screenshots/neon_1.jpg" },
                new() { Id = 4, GameId = 3, Url = "https://example.com/screenshots/elden_1.jpg" }
            });

            // 6. Сід версій ігор
            modelBuilder.Entity<GameVersion>().HasData(new List<GameVersion>()
            {
                new() { Id = 1, GameId = 1, Version = "v1.0", PatchNotes = "Initial release." },
                new() { Id = 2, GameId = 1, Version = "v1.1", PatchNotes = "Fixed smoke grenades." },
                new() { Id = 3, GameId = 2, Version = "v0.9", PatchNotes = "Early Access Launch." },
                new() { Id = 4, GameId = 3, Version = "v1.10", PatchNotes = "Colosseum update." }
            });
        }
    }
}
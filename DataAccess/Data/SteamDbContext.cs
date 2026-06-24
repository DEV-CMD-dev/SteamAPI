using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Data
{
    public class SteamDbContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;

        public SteamDbContext(DbContextOptions<SteamDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<Game> Game { get; set; }
    
        public DbSet<Achievement> Achievement { get; set; }
        public DbSet<OwnedGame> OwnedGames { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }
        public DbSet<GameVersion> GameVersions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           base.OnConfiguring(optionsBuilder);
           optionsBuilder.UseSqlServer(_configuration.GetConnectionString("RemoteDb"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.SeedSteamData();

        }
    }
}

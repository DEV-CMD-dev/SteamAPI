using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SteamDbContext : IdentityDbContext<User>
    {
        public SteamDbContext(DbContextOptions<SteamDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
        public DbSet<GameVersion> GameVersions { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Profile> Profiles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //User
            builder.Entity<User>()
                .Property(u => u.WalletBalance)
                .HasPrecision(18, 2);

            builder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //User - game
            builder.Entity<UserGame>()
                .HasKey(ug => new { ug.UserId, ug.GameId });

            builder.Entity<UserGame>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGames)
                .HasForeignKey(ug => ug.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserGame>()
                .HasOne(ug => ug.Game)
                .WithMany(g => g.UserGames)
                .HasForeignKey(ug => ug.GameId);

            // Game
            builder.Entity<Game>()
                .Property(g => g.Price)
                .HasPrecision(18, 2);

            builder.Entity<Game>()
                .HasOne(g => g.Developer)
                .WithMany(u => u.DevelopedGames)
                .HasForeignKey(g => g.DeveloperId);

            builder.Entity<Game>()
                .HasMany(g => g.UserWishlist)
                .WithMany(t => t.Wishlist);

            builder.Entity<Game>()
              .HasMany(g => g.UserCart)
              .WithMany(t => t.Cart);


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

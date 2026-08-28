using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Data.Entities.DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DataAccess
{
    public class SteamDbContext : IdentityDbContext<User>
    {
        public SteamDbContext(DbContextOptions<SteamDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<GameVersion> GameVersions { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<TradeOffer> TradeOffers { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //User
            builder.Entity<User>()
                .Property(u => u.WalletBalance)
                .HasPrecision(18, 2);

            builder.Entity<Profile>()
                .HasKey(p => p.UserId);

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
                .Property(g => g.RecommendationPercentage)
                .HasPrecision(5, 2);

            builder.Entity<Game>()
                .HasOne(g => g.Developer)
                .WithMany(u => u.DevelopedGames)
                .HasForeignKey(g => g.DeveloperId);

            // Wishlist
            builder.Entity<Wishlist>()
                .HasKey(w => new { w.UserId, w.GameId });

            builder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wishlists) 
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Wishlist>()
                .HasOne(w => w.Game)
                .WithMany(g => g.Wishlists) 
                .HasForeignKey(w => w.GameId);

            // Cart
            builder.Entity<Cart>()
                .HasKey(w => new { w.UserId, w.GameId });

            builder.Entity<Cart>()
                .HasOne(w => w.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Cart>()
                .HasOne(w => w.Game)
                .WithMany(g => g.Carts)
                .HasForeignKey(w => w.GameId);

            // Order
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);
         
            // OrderItem
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Game)
                .WithMany(g => g.OrderItems)
                .HasForeignKey(oi => oi.GameId);

            builder.Entity<OrderItem>()
             .Property(o => o.Price)
             .HasPrecision(18, 2);

            // Review
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(r => r.Game)
                .WithMany(g => g.Reviews)
                .HasForeignKey(r => r.GameId);

            builder.Entity<Review>()
                .HasIndex(r => new { r.UserId, r.GameId })
                .IsUnique();

            // User - Achievement
            builder.Entity<User>()
                .HasMany(w => w.Achievements)
                .WithMany(w => w.Users)
                .UsingEntity<UserAchievement>();


            builder.Entity<Item>()
                .HasOne(i => i.Game)
                .WithMany(g => g.Items) 
                .HasForeignKey(i => i.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<InventoryItem>()
                .HasOne(i => i.User)
                .WithMany(u => u.Inventory) 
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // TradeOffer
            builder.Entity<TradeOffer>()
                .HasOne(t => t.Sender)
                .WithMany()
                .HasForeignKey(t => t.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TradeOffer>()
                .HasOne(t => t.Receiver)
                .WithMany()
                .HasForeignKey(t => t.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            //Friendship
            builder.Entity<Friendship>()
                .HasKey(f => new { f.UserId, f.FriendId });

           
            builder.Entity<Friendship>()
                .HasOne(f => f.User)
                .WithMany() 
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

   
            builder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);


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

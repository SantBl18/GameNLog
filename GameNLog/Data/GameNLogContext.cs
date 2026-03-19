using GameNLog.Models;
using Microsoft.EntityFrameworkCore;

namespace GameNLog.Data
{
    public class GameNLogContext : DbContext
    {
        public GameNLogContext(DbContextOptions<GameNLogContext> options) : base(options) { }
        public DbSet<Game> Games { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<InvolvedCompany> InvolvedCompanies { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<GameLog> PlayedGames { get; set; }
        public DbSet<GameReview> PlayedGameReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<InvolvedCompany>()
                .HasKey(c => new { c.GameID, c.CompanyID });

            modelBuilder.Entity<GameGenre>()
                .HasKey(c => new { c.GameID, c.GenreID });

            modelBuilder.Entity<GamePlatform>()
                .HasKey(c => new { c.GameID, c.PlatformID });

            modelBuilder.Entity<GameLog>()
                .HasIndex(c => new { c.GameID, c.UserID }).IsUnique();
        }
    }
}

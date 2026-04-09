using GameNLog.Models;
using Microsoft.EntityFrameworkCore;

namespace GameNLog.Data
{
    public class GameNLogContext : DbContext
    {
        public GameNLogContext(DbContextOptions<GameNLogContext> options) : base(options) { }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameCover> Covers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<InvolvedCompany> InvolvedCompanies { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<GameLog> GameLogs { get; set; }
        public DbSet<GameReview> GameReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameNLogContext).Assembly);
        }
    }
}

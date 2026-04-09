using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameNLog.Models
{
    public class Game
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Summary { get; set; }
        public DateTime? FirstReleaseDate { get; set; }
        public GameCover? Cover { get; set; }
        public double? AverageRating { get; set; }
        public int RatingCount { get; set; }
        public double? BayesianRating { get; set; }
        public ICollection<GameLog> GameLogs { get; set; } = [];
        public ICollection<GameReview> GameReviews { get; set; } = [];
        public ICollection<GamePlatform> GamePlatforms { get; set; } = [];
        public ICollection<GameGenre> GameGenres { get; set; } = [];
        public ICollection<InvolvedCompany> InvolvedCompanies { get; set; } = [];
    }

    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game");

            builder.Property(g => g.Id).ValueGeneratedNever();
            builder.Property(g => g.FirstReleaseDate).ValueGeneratedNever();

            builder.Property(g => g.BayesianRating)
                .HasComputedColumnSql(
                @"(
                    (""RatingCount""::double precision / (""RatingCount"" + 20)) * COALESCE(""AverageRating"", 0) +
                (20 / (""RatingCount"" + 20)) * 6.5
                )",
                stored: true);

            builder.HasIndex(g => g.BayesianRating);

        }
    }
}

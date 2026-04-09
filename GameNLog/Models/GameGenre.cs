using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameNLog.Models
{
    public class GameGenre
    {
        public int GameId { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
        public Game Game { get; set; } = null!;
    }

    public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            builder.ToTable("GameGenre");
            builder.HasKey(gg => new { gg.GameId, gg.GenreId });

            builder.HasOne(gg => gg.Game)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GameId);

            builder.HasOne(gg => gg.Genre)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GenreId);
        }
    }
}

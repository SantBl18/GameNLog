using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameNLog.Models
{
    public class GameCover
    {
        public int Id { get; set; }
        public required string ImageId { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
    }

    public class CoverConfiguration : IEntityTypeConfiguration<GameCover>
    {
        public void Configure(EntityTypeBuilder<GameCover> builder)
        {
            builder.ToTable("GameCover");
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.HasOne(gc => gc.Game)
                .WithOne(g => g.Cover)
                .HasForeignKey<GameCover>(gc => gc.GameId)
                .IsRequired();
        }
    }
}

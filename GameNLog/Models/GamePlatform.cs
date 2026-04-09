using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameNLog.Models
{
    public class GamePlatform
    {
        public int GameId { get; set; }
        public int PlatformId { get; set; }
        public Game Game { get; set; } = null!;
        public Platform Platform { get; set; } = null!;
    }

    public class GamePlatformConfiguration : IEntityTypeConfiguration<GamePlatform>
    {
        public void Configure(EntityTypeBuilder<GamePlatform> builder)
        {
            builder.ToTable("GamePlatform");
            builder.HasKey(gp => new { gp.GameId, gp.PlatformId });

            builder.HasOne(gp => gp.Game)
                .WithMany(g => g.GamePlatforms)
                .HasForeignKey(gp => gp.GameId);

            builder.HasOne(gp => gp.Platform)
                .WithMany(g => g.GamePlatforms)
                .HasForeignKey(gg => gg.PlatformId);
        }
    }
}

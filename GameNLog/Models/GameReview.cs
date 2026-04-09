using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameNLog.Models
{
    public class GameReview
    {
        public int Id { get; set; }
        public int GameLogId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime ReviewedAt { get; set; } = DateTime.UtcNow;
        public GameLog GameLog { get; set; } = null!;
    }

    public class GameReviewConfiguration : IEntityTypeConfiguration<GameReview>
    {
        public void Configure(EntityTypeBuilder<GameReview> builder)
        {
            builder.ToTable("GameReview");

            builder.HasOne(r => r.GameLog)
                .WithOne(gl => gl.Review)
                .HasForeignKey<GameReview>(r => r.GameLogId);

            builder.HasIndex(r => r.GameLogId)
                .IsUnique();

            builder.ToTable("GameReview", t =>
            {
                t.HasCheckConstraint("CK_GameReview_Description_Length",
                    @"LENGTH(TRIM(""Description"")) > 0 AND LENGTH(""Description"") <= 1024");
                t.HasCheckConstraint("CK_GameReview_ReviewedAt",
                    @"""ReviewedAt"" <= NOW()");

            });
        }
    }
}

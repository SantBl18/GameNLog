using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameNLog.Models
{
    public enum LogType
    {
        Playing,
        Completed,
        Paused,
        Dropped,
        Wishlist,
        Blacklist
    }
    public class GameLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int? Score { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
        public LogType LogType { get; set; }
        public bool IsFavorite { get; set; }
        public User User { get; set; } = null!;
        public Game Game { get; set; } = null!;
        public GameReview? Review { get; set; }
    }

    public class GameLogConfiguration : IEntityTypeConfiguration<GameLog>
    {
        public void Configure(EntityTypeBuilder<GameLog> builder)
        {
            builder.ToTable("GameLog");

            builder.HasOne(gl => gl.User)
                .WithMany(u => u.GameLogs)
                .HasForeignKey(gl => gl.UserId);

            builder.HasOne(gl => gl.Game)
                .WithMany(g => g.GameLogs)
                .HasForeignKey(gl => gl.GameId);

            builder.HasIndex(gl => new { gl.UserId, gl.GameId })
                .IsUnique();

            builder.ToTable("GameLog", t =>
            {
                t.HasCheckConstraint("CK_GameLog_Score_Range",
                    @"""Score"" IS NULL OR (""Score"" >= 1 AND ""Score"" <= 10)");
                t.HasCheckConstraint("CK_GameLog_LoggedAt",
                    @"""LoggedAt"" <= NOW()");

            });
        }
    }

}

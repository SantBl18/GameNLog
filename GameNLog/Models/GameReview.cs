using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    public class GameReview
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PlayedGameID")]
        public int GameLogId { get; set; }
        [Required]
        public int Score { get; set; }
        public string? Description { get; set; }
        public required GameLog PlayedGame { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ReviewedAt { get; set; }

    }
}

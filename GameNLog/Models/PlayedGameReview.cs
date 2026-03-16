using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    public class PlayedGameReview
    {
        [Key]
        public int ReviewID { get; set; }
        [ForeignKey("PlayedGameID")]
        public int PlayedGameID { get; set; }
        [Required]
        public int Score { get; set; }
        public string? Description { get; set; }
        public required PlayedGame PlayedGame { get; set; }

    }
}

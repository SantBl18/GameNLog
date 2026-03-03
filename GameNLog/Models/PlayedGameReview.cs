using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    public class PlayedGameReview
    {
        [ForeignKey("PlayedGameID")]
        public int PlayedGameID { get; set; }
        public int Score { get; set; }
        public string? Description { get; set; }
    }
}

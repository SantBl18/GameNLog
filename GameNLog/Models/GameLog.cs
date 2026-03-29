using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    [Table("GameLog")]
    public class GameLog
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public int GameID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LoggedAt { get; set; }
        public bool IsFavorite { get; set; }
        public User? User { get; set; }
        public Game? Game { get; set; }
        public ICollection<GameReview> Reviews { get; } = new List<GameReview>();
    }
}

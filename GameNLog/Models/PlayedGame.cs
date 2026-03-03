using System.ComponentModel.DataAnnotations;

namespace GameNLog.Models
{
    public class PlayedGame
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int GameID { get; set; }
        [DataType(DataType.DateTime)]
        public int PlayedAt { get; set; }
    }
}

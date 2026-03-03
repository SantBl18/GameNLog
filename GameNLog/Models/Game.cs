using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    public class Game
    {
        [Key]
        public int GameID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? slug { get; set; }
        [Required]
        public string? summary { get; set; }
        [ForeignKey("CoverID")]
        public int CoverID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FirstReleaseDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GameID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Slug { get; set; }
        [Required]
        public required string Summary { get; set; }
        [ForeignKey("CoverID")]
        public int CoverID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FirstReleaseDate { get; set; }
        public Cover? Cover { get; set; }
        public ICollection<PlayedGame> PlayedGames { get; } = [];
        public ICollection<GamePlatform> GamePlatforms { get; } = [];
        public ICollection<GameGenre> GameGenres { get; } = [];
        public ICollection<InvolvedCompany> InvolvedCompanies { get; } = [];

    }
}

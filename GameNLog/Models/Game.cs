using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    public class Game
    {
        [Key]
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
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        public required Cover GameCover { get; set; }
        public ICollection<PlayedGame> PlayedGames { get; } = new List<PlayedGame>();
        public ICollection<GamePlatform> GamePlatforms { get; } = new List<GamePlatform>();
        public ICollection<GameGenre> GameGenres { get; } = new List<GameGenre>();
        public ICollection<GameCompany> GameCompanies { get; } = new List<GameCompany>();

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Slug { get; set; }
        public ICollection<GameGenre> GameGenres { get; } = [];
    }
}

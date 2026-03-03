using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Slug { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}

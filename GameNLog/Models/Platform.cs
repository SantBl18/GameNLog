using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    [Table("Platform")]
    public class Platform
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Slug { get; set; }
        [Required]
        public required string Abbreviation { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; } = [];

    }
}

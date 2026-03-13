using System.ComponentModel.DataAnnotations;

namespace GameNLog.Models
{
    public class Platform
    {
        [Key]
        public int PlatformID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Slug { get; set; }
        [Required]
        public required string Abbreviation { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; } = [];

    }
}

using System.ComponentModel.DataAnnotations;

namespace GameNLog.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Email { get; set; }
        public string? Biography { get; set; }
        public ICollection<PlayedGame> PlayedGames { get; } = [];
    }
}

using System.ComponentModel.DataAnnotations;

namespace GameNLog.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Biography { get; set; }
    }
}

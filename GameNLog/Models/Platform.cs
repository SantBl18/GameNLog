using System.ComponentModel.DataAnnotations;

namespace GameNLog.Models
{
    public class Platform
    {
        [Key]
        public int PlatformID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Slug { get; set; }
        [Required]
        public string? Summary { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;

namespace GameNLog.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}

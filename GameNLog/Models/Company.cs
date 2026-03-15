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
        [Required]
        public required string Slug { get; set; }
        public ICollection<InvolvedCompany> InvolvedCompanies{ get; } = [];

    }
}

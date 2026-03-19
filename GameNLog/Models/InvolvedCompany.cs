using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    [Table("InvolvedCompany")]
    public class InvolvedCompany
    {
        public int GameID { get; set; }
        public int CompanyID { get; set; }
        public Game? Game { get; set; }
        public Company? Company { get; set; }
    }
}

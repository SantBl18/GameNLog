namespace GameNLog.Models
{
    public class InvolvedCompany
    {
        public int GameID { get; set; }
        public int CompanyID { get; set; }
        public required Game Game { get; set; }
        public required Company Company { get; set; }
    }
}

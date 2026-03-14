namespace GameNLog.Models
{
    public class InvolvedCompany
    {
        public int GameID { get; set; }
        public int CompanyID { get; set; }
        public Game? Game { get; set; }
        public Company? Company { get; set; }
    }
}

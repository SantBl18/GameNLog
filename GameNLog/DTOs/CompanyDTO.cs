namespace GameNLog.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Slug { get; set; }
    }
}

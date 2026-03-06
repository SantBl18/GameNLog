namespace GameNLog.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}

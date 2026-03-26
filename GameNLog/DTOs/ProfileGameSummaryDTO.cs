namespace GameNLog.DTOs
{
    public class ProfileGameSummaryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? CoverURL { get; set; }
    }
}

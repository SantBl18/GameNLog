namespace GameNLog.DTOs
{
    public class GameSummaryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? CoverURL { get; set; }
        public double? AverageRating { get; set; }
    }
}

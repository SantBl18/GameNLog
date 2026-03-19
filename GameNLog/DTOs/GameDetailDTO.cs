namespace GameNLog.DTOs
{
    public class GameDetailDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Summary { get; set; }
        public string? CoverURL { get; set; }
        public List<string>? Genres { get; set; }
        public List<string>? Platforms { get; set; }
        public List<string>? Companies { get; set; }
        public List<ReviewDTO>? RecentReviews { get; set; }
        public double? AverageRating { get; set; }
        public int ReviewCount { get; set; }

    }
}

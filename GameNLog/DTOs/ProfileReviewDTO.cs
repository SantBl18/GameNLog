namespace GameNLog.DTOs
{
    public class ProfileReviewDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public required ProfileGameSummaryDTO Game { get; set; }
    }
}

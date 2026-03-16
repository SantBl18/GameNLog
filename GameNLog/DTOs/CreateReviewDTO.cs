namespace GameNLog.DTOs
{
    public class CreateReviewDTO
    {
        public int PlayedGameId { get; set; }
        public int Rating { get; set; }
        public string? Description { get; set; }
    }
}

namespace GameNLog.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Username { get; set; }
        public int Rating { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

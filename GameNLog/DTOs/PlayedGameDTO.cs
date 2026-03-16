namespace GameNLog.DTOs
{
    public class PlayedGameDTO
    {
        public required GameSummaryDTO Game { get; set; }
        public DateTime DatePlayed { get; set; }
        public ReviewDTO? Review { get; set; }
    }
}

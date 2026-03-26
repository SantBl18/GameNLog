namespace GameNLog.DTOs
{
    public class UserProfileDTO
    {
        public required UserInfoDTO UserInfo { get; set; }
        public List<ProfileReviewDTO> RecentlyReviewed { get; set; } = [];
        public List<GameSummaryDTO> RecentlyPlayed { get; set; } = [];
        public List<GameSummaryDTO> FavoriteGames { get; set; } = [];
    }

    public class UserInfoDTO
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public string? Biography { get; set; }
    }
}

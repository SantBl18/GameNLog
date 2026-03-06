namespace GameNLog.DTOs
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public string? Biography { get; set; }

    }
}

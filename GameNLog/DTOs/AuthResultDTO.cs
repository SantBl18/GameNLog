namespace GameNLog.DTOs
{
    public class AuthResultDTO
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? Error { get; set; }
    }
}

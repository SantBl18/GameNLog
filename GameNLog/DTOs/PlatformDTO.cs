namespace GameNLog.DTOs
{
    public class PlatformDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Abbreviation { get; set; }
    }
}

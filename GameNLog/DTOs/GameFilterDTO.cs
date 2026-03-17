namespace GameNLog.DTOs
{
    public class GameFilterDTO
    {   public List<int>? Genres { get; set; }
        public List<int>? Platforms { get; set; }
        public List<int>? Companies { get; set; }
        public DateTime? ReleaseDateFrom { get; set; }
        public DateTime? ReleaseDateTo { get; set; }
        public string? SearchString { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}

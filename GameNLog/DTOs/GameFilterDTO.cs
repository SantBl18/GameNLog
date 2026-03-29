using Microsoft.AspNetCore.Mvc;

namespace GameNLog.DTOs
{
    public class GameFilterDTO
    {
        public List<int> Genres { get; set; } = [];
        public List<int> Platforms { get; set; } = [];
        public List<int> Companies { get; set; } = [];
        [FromQuery(Name = "from")]
        public DateTime? ReleaseDateFrom { get; set; }
        [FromQuery(Name = "to")]
        public DateTime? ReleaseDateTo { get; set; }
        [FromQuery(Name = "search")]
        public string? SearchString { get; set; }
        public string? SortBy { get; set; }
        public bool SortDesc { get; set; } = false;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}

namespace GameNLog.DTOs
{
    public class PaginatedResultDTO<T>
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; } = [];
    }
}

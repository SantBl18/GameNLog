namespace GameNLog.DTOs
{
    public class PaginatedResultDTO<T>
    {
        public int PageSize { get; set; }
        public List<T> Data { get; set; } = [];
    }
}

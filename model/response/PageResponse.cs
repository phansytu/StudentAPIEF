namespace StudentAPIw6.Model.response
{
    public class PageResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Data { get; set; } = new List<T>();
        public int TotalPages { get; set; }
    }
}
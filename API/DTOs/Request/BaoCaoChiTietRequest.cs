namespace StudentAPIw6.API.DTOs.Request
{
    public class BaoCaoChiTietRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Keyword { get; set; }
        public int? LopHocId { get; set; }
        public int? BoMonId { get; set; }
    }
}
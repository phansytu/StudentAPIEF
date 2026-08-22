namespace StudentAPIw6.Model.request
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
//nhan du lien phan trang tu client gui len, mac dinh page = 1 
// va pageSize = 5
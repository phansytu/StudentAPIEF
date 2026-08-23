using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw6.API.DTOs.Request;

namespace StudentAPIw6.API.DTOs.Request
{
    public class SinhVienQueryRequest : PaginationRequest
    {
        public string? Keyword { get; set; }

        public bool? GioiTinh { get; set; }

        public decimal? DiemTu { get; set; }

        public decimal? DiemDen { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; } = false;
    }
}
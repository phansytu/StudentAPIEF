using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw5.model.request
{
    public class SinhVienQueryRequest : PaginationRequest
    {
        public string? Keyword { get; set; }

        public bool? GioiTinh { get; set; }

        public double? DiemTu { get; set; }

        public double? DiemDen { get; set; }

        public string? SortBy { get; set; }

        public bool Descending { get; set; } = false;
    }
}
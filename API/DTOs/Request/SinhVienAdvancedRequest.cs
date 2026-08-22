using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.API.DTOs.Request
{

    public class SinhVienAdvancedRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? Keyword { get; set; }

        public int? LopHocId { get; set; }

        public int? BoMonId { get; set; }

        public double? MinDiem { get; set; }

        public double? MaxDiem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw5.model
{
    public class LopHoc
    {
        public string Id { get; set; } = string.Empty;
        public string MaLop { get; set; } = string.Empty;
        public required string TenLop { get; set; }
        public required string ChuyenNganh { get; set; }

        public ICollection<SinhVien>? SinhViens { get; set; }
    }
}
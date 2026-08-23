using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.API.DTOs.response
{
    public class ThongKeLopHoc
    {
        public int lopHocId { get; set; }

        public string TenLop { get; set; } = string.Empty;

        public string ChuyenNganh { get; set; } = string.Empty;

        public int SoLuongSinhVien { get; set; }

        public decimal DiemTrungBinh { get; set; }

        public decimal DiemCaoNhat { get; set; }

        public decimal DiemThapNhat { get; set; }
    }
}
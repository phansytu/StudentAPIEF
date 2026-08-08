using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw5.model.response
{
    public class ThongKeLopHoc
    {
        public string MaLop { get; set; } = string.Empty;

        public string TenLop { get; set; } = string.Empty;

        public string ChuyenNganh { get; set; } = string.Empty;

        public int SoLuongSinhVien { get; set; }

        public double DiemTrungBinh { get; set; }

        public double DiemCaoNhat { get; set; }

        public double DiemThapNhat { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw5.model
{
    public class SinhVien
    {

        public string Id { get; set; } = string.Empty;
        public string MaSV { get; set; } = string.Empty;
        public required string HoTen { get; set; }
        public bool GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public required string Email { get; set; }
        public double DiemTB { get; set; }
        public required string MaLop { get; set; }
        public LopHoc? lopHoc { get; set; }

    }
}
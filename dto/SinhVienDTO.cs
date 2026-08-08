using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw5.dto
{
    public class SinhVienDTO
    {
        public class Response
        {
            public string? MaSV { get; set; }
            public string? HoTen { get; set; }
            public bool GioiTinh { get; set; }
            public DateTime NgaySinh { get; set; }
            public string? Email { get; set; }
            public double DiemTB { get; set; }
            public string? MaLop { get; set; }
        }



        public class SinhVienCreateDTO
        {
            public required string HoTen { get; set; }
            public bool GioiTinh { get; set; }
            public DateTime NgaySinh { get; set; }
            public required string Email { get; set; }
            public double DiemTB { get; set; }
            public required string MaLop { get; set; }

        }
        public class SinhVienUpdateDTO
        {
            public required string HoTen { get; set; }
            public bool GioiTinh { get; set; }
            public DateTime NgaySinh { get; set; }
            public required string Email { get; set; }
            public double DiemTB { get; set; }
            public required string MaLop { get; set; }

        }
    }
}
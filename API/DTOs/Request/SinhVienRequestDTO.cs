using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.API.DTOs.Request
{
    public class SinhVienRequestDTO
    {
        public class SinhVienCreateDTO
        {
            public required string HoTen { get; set; }
            public bool GioiTinh { get; set; }
            public DateTime NgaySinh { get; set; }
            public required string Email { get; set; }
            public decimal DiemTB { get; set; }
            public required int lopHocId { get; set; }

        }
        public class SinhVienUpdateDTO
        {
            public required string HoTen { get; set; }
            public bool GioiTinh { get; set; }
            public DateTime NgaySinh { get; set; }
            public required string Email { get; set; }
            public decimal DiemTB { get; set; }
            public required int lopHocId { get; set; }

        }

    }
}
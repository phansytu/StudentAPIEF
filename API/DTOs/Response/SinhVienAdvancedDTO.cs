using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.API.DTOs.Response
{
    public class SinhVienAdvancedDTO
    {
        public int Id { get; set; }

        public string Msv { get; set; } = string.Empty;

        public string HoTen { get; set; } = string.Empty;

        public bool GioiTinh { get; set; }

        public DateTime NgaySinh { get; set; }

        public string? Email { get; set; }

        public decimal DiemTb { get; set; }

        public string TenLop { get; set; } = string.Empty;

        public string TenMon { get; set; } = string.Empty;
    }
}
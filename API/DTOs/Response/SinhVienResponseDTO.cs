using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.API.DTOs.Response
{
    public class SinhVienResponseDTO
    {

        public int Id { get; set; }
        public string? MaSV { get; set; }
        public string? HoTen { get; set; }
        public bool GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string? Email { get; set; }
        public decimal DiemTB { get; set; }
        public int LopHocId { get; set; }


    }
}
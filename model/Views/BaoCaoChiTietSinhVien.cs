
using Microsoft.EntityFrameworkCore;

namespace StudentAPIw6.Model.Views
{
    [Keyless]
    public class BaoCaoChiTietSinhVien
    {
        public int SinhVienId { get; set; }
        public string MaSinhVien { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public bool GioiTinh { get; set; }
        public string GioiTinhText { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; }
        public int Tuoi { get; set; }
        public string Email { get; set; } = string.Empty;
        public double DiemTB { get; set; }
        public string XepLoai { get; set; } = string.Empty;
        public int? LopHocId { get; set; }
        public string MaLop { get; set; } = string.Empty;
        public string TenLop { get; set; } = string.Empty;
        public string ChuyenNganh { get; set; } = string.Empty;
        public int? BoMonId { get; set; }
        public string TenBoMon { get; set; } = string.Empty;
    }
}
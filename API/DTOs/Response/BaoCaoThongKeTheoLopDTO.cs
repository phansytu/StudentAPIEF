namespace StudentAPIw6.API.DTOs.Response
{

    public class BaoCaoThongKeTheoLopDTO
    {
        public int LopHocId { get; set; }
        public string MaLop { get; set; } = string.Empty;
        public string TenLop { get; set; } = string.Empty;
        public string ChuyenNganh { get; set; } = string.Empty;
        public string TenBoMon { get; set; } = string.Empty;
        public int TongSoSinhVien { get; set; }
        public int SoNam { get; set; }
        public int SoNu { get; set; }
        public double DiemTrungBinhLop { get; set; }
        public double DiemCaoNhat { get; set; }
        public double DiemThapNhat { get; set; }
    }
}
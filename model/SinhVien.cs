
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAPIw6.Model
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        [Column("id")]

        public string Id { get; set; } = string.Empty;
        [Column("msv")]
        public string MaSV { get; set; } = string.Empty;
        [Column("hoTen")]
        public required string HoTen { get; set; }
        [Column("gioiTinh")]
        public bool GioiTinh { get; set; }
        [Column("ngaySinh")]
        public DateTime NgaySinh { get; set; }
        [Column("email")]
        public required string Email { get; set; }
        [Column("diemTb")]
        public double DiemTB { get; set; }
        [Column("maLop")]
        public required string MaLop { get; set; }
        [ForeignKey("MaLop")]
        public LopHoc? lopHoc { get; set; }

    }
}
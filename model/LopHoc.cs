
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAPIw6.Model
{
    [Table("LopHoc")]
    public class LopHoc
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("maLop")]

        public string MaLop { get; set; } = string.Empty;
        [Column("tenLop")]
        public required string TenLop { get; set; }
        [Column("chuyenNganh")]
        public required string ChuyenNganh { get; set; }


        [Column("boMonId")]
        public int BoMonId { get; set; }
        [ForeignKey("BoMonId")]
        public BoMon? BoMon { get; set; }
        public ICollection<SinhVien>? SinhViens { get; set; }
    }
}
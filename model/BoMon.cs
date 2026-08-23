
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAPIw6.Model
{
    [Table("BoMon")]
    public class BoMon
    {
        [Key]
        public int id { get; set; }
        public string maBM { get; set; } = string.Empty;
        [Column("tenMon")]
        public string tenBM { get; set; } = string.Empty;
        public ICollection<LopHoc>? lopHocs { get; set; }
    }
}
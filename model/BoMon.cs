
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAPIw6.Model
{
    [Table("BoMon")]
    public class BoMon
    {
        [Key]
        public int id { get; set; }

        public required string maBM { get; set; }
        [Column("tenMon")]
        public string tenBM { get; set; } = string.Empty;
        public ICollection<LopHoc>? lopHocs { get; set; }
    }
}
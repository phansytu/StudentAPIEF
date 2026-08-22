using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.API.DTOs.Response
{
    public class LopHocResponseDTO
    {
        public int Id { get; set; }

        public string MaLop { get; set; } = null!;
        public required string TenLop { get; set; }
        public required string ChuyenNganh { get; set; }
        public required int BoMonId { get; set; }

    }
}
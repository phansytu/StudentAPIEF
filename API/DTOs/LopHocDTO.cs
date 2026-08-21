using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.DTOs
{
    public class LopHocDTO
    {


        public class Response
        {
            public int Id { get; set; }

            public string MaLop { get; set; } = null!;
            public required string TenLop { get; set; }
            public required string ChuyenNganh { get; set; }
            public required int BoMonId { get; set; }
        }
        public class LopHocCreateDTO
        {
            public required string TenLop { get; set; }
            public required string ChuyenNganh { get; set; }
            public required int boMonId { get; set; }
        }
        public class LopHocUpdateDTO
        {
            public required string TenLop { get; set; }
            public required string ChuyenNganh { get; set; }
            public required int boMonId { get; set; }
        }
        public class LopHocDeleteDTO
        {
            public required string MaLop { get; set; }

        }

    }
}
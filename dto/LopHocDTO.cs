using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw5.dto
{
    public class LopHocDTO
    {


        public class Response
        {
            public required string MaLop { get; set; }
            public required string TenLop { get; set; }
            public required string ChuyenNganh { get; set; }
        }
        public class LopHocCreateDTO
        {

            public required string TenLop { get; set; }
            public required string ChuyenNganh { get; set; }
        }
        public class LopHocUpdateDTO
        {
            public required string TenLop { get; set; }
            public required string ChuyenNganh { get; set; }
        }
        public class LopHocDeleteDTO
        {
            public required string MaLop { get; set; }
            public required string TenLop { get; set; }
            public required string ChuyenNganh { get; set; }
        }

    }
}
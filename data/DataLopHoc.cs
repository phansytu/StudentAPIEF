using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw5.model;
namespace StudentAPIw5.data
{
    public class DataLopHoc
    {
        public readonly List<LopHoc> LopHocs = new List<LopHoc>
        {
            new LopHoc { MaLop = "L001", TenLop = "Lop 1", ChuyenNganh = "Công nghệ thông tin" },
            new LopHoc { MaLop = "L002", TenLop = "Lop 2", ChuyenNganh = "Điện tử - Viễn thông" },
            new LopHoc { MaLop = "L003", TenLop = "Lop 3", ChuyenNganh = "Khoa học máy tính" },
            new LopHoc { MaLop = "L004", TenLop = "Lop 4", ChuyenNganh = "Công nghệ phần mềm" }

        };
    }
}
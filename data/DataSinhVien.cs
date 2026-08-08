using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw5.model;
namespace StudentAPIw5.data
{
    public class DataSinhVien
    {
        public List<SinhVien> SinhViens = new List<SinhVien>
        {
            new SinhVien
            {

                HoTen = "Nguyen Van A",
                NgaySinh = new DateTime(2000, 1, 1),
                GioiTinh = true,
                Email = "a@gmail.com",
                DiemTB = 8.0,
                MaLop = "L001"
            },
            new SinhVien
            {

                HoTen = "Tran Thi B",
                NgaySinh = new DateTime(2001, 2, 2),
                GioiTinh = false,
                Email = "b@gmail.com",
                DiemTB = 7.5,
                MaLop = "L002"
            },
            new SinhVien
            {

                HoTen = "Le Van C",
                NgaySinh = new DateTime(2002, 3, 3),
                GioiTinh = true,
                Email = "7c@gmail.com",
                DiemTB = 9.0,
                MaLop = "L001"
            },
            new SinhVien
            {

                HoTen = "Pham Thi D",
                NgaySinh = new DateTime(2003, 4, 4),
                GioiTinh = false,
                Email = "d@gmail.com",
                DiemTB = 5.5,
                MaLop = "L002"
            },
            new SinhVien {HoTen = "Nguyen Van E", NgaySinh = new DateTime(2004, 5, 5), GioiTinh = true, Email ="e@gmail.com",DiemTB=8.4, MaLop = "L001"},
            new SinhVien {HoTen = "Tran Thi F", NgaySinh = new DateTime(2005, 6, 6), GioiTinh = false, Email ="f@gmail.com",DiemTB=9.5, MaLop = "L002"},
            new SinhVien {HoTen = "Le Van G", NgaySinh = new DateTime(2006, 7, 7), GioiTinh = true, Email ="g@gmail.com",DiemTB=6.4, MaLop = "L001"},
            new SinhVien {HoTen = "Pham Thi H", NgaySinh = new DateTime(2007, 8, 8), GioiTinh = false, Email ="H@gmail.com",DiemTB=7.23, MaLop = "L004"},
            new SinhVien {HoTen = "Nguyen Van I", NgaySinh = new DateTime(2008, 9, 9), GioiTinh = true, Email ="i@gmail.com",DiemTB=5.54, MaLop = "L003"},
            new SinhVien {HoTen = "Tran Thi J", NgaySinh = new DateTime(2005, 10, 10), GioiTinh = false, Email ="j@gmail.com",DiemTB=8.2, MaLop = "L002"},
            new SinhVien {HoTen = "Le Van K", NgaySinh = new DateTime(2005, 11, 11), GioiTinh = true, Email ="k@gmail.com",DiemTB=8.9, MaLop = "L001"},
            new SinhVien {HoTen = "Pham Thi L", NgaySinh = new DateTime(2005, 12, 12), GioiTinh = false, Email ="l@gmail.com",DiemTB=8.2, MaLop = "L002"},
            new SinhVien {HoTen = "Nguyen Van M", NgaySinh = new DateTime(2005, 1, 13), GioiTinh = true, Email ="m@gmail.com",DiemTB=5.1, MaLop = "L001"},
            new SinhVien {HoTen = "Tran Thi N", NgaySinh = new DateTime(2005, 2, 14), GioiTinh = false, Email ="n@gmail.com",DiemTB=9.64, MaLop = "L002"},
            new SinhVien {HoTen = "Le Van O", NgaySinh = new DateTime(2005, 3, 15), GioiTinh = true, Email ="0@gmail.com",DiemTB=8.2, MaLop = "L001"},
            new SinhVien {HoTen = "Pham Thi P", NgaySinh = new DateTime(2005, 4, 16), GioiTinh = false, Email ="p@gmail.com",DiemTB=3.7, MaLop = "L002"},
            new SinhVien {HoTen = "Nguyen Van Q", NgaySinh = new DateTime(2005, 5, 17), GioiTinh = true, Email ="qw@gmail.com",DiemTB=7.4, MaLop = "L003"},
            new SinhVien {HoTen = "Tran Thi R", NgaySinh = new DateTime(2005, 6, 18), GioiTinh = false, Email ="re@gmail.com",DiemTB=6.4, MaLop = "L004"},
            new SinhVien {HoTen = "Le Van S", NgaySinh = new DateTime(2005, 7, 19), GioiTinh = true, Email ="sfdf@gmail.com",DiemTB=4.4, MaLop = "L003"},
            new SinhVien {HoTen = "Pham Thi T", NgaySinh = new DateTime(2005, 8, 20), GioiTinh = false, Email ="e3@gmail.com",DiemTB=8.3, MaLop = "L004"},
            new SinhVien {HoTen = "Nguyen Van U", NgaySinh = new DateTime(2005, 9, 21), GioiTinh = true, Email ="acae@gmail.com",DiemTB=5.4, MaLop = "L003"},
            new SinhVien {HoTen = "Tran Thi V", NgaySinh = new DateTime(2005, 10, 22), GioiTinh = false, Email ="e3fd@gmail.com",DiemTB=8.2, MaLop = "L003"},
            new SinhVien {HoTen = "Le Van W", NgaySinh = new DateTime(2005, 11, 23), GioiTinh = true, Email ="fldl@gmail.com",DiemTB=8.4, MaLop = "L004"}        };
    }
}

using StudentAPIw6.Model;
using StudentAPIw6.DTOs;


namespace StudentAPIw6.AutoMapper
{
    public static class StudentMapper
    {
        // create -> entity
        public static SinhVien ToEntity(this SinhVienDTO.SinhVienCreateDTO createStudentDTO)
        {
            return new SinhVien
            {

                HoTen = createStudentDTO.HoTen,
                NgaySinh = createStudentDTO.NgaySinh,
                GioiTinh = createStudentDTO.GioiTinh,
                Email = createStudentDTO.Email,
                DiemTB = createStudentDTO.DiemTB,
                MaLop = createStudentDTO.MaLop
            };
        }
        // entity -> response
        public static SinhVienDTO.Response ToResponse(this SinhVien student)
        {
            return new SinhVienDTO.Response
            {
                MaSV = student.MaSV,
                HoTen = student.HoTen,
                NgaySinh = student.NgaySinh,
                GioiTinh = student.GioiTinh,
                Email = student.Email,
                DiemTB = student.DiemTB,
                MaLop = student.MaLop
            };
        }
        // update -> entity
        public static void updateEntity(this SinhVien student, SinhVienDTO.SinhVienUpdateDTO updateStudentDTO)
        {

            student.HoTen = updateStudentDTO.HoTen;
            student.NgaySinh = updateStudentDTO.NgaySinh;
            student.GioiTinh = updateStudentDTO.GioiTinh;
            student.Email = updateStudentDTO.Email;
            student.DiemTB = updateStudentDTO.DiemTB;
            student.MaLop = updateStudentDTO.MaLop;
        }
        // entity -> response
        public static List<SinhVienDTO.Response> ToResponseList(this IEnumerable<SinhVien> students)
        {
            return students.Select(student => student.ToResponse()).ToList();

        }


    }
}
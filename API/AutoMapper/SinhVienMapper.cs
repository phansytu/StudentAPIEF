
using StudentAPIw6.Model;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.DTOs.Response;


namespace StudentAPIw6.AutoMapper
{
    public static class StudentMapper
    {
        // create -> entity
        public static SinhVien ToEntity(this SinhVienRequestDTO.SinhVienCreateDTO createStudentDTO)
        {
            return new SinhVien
            {

                HoTen = createStudentDTO.HoTen,
                NgaySinh = createStudentDTO.NgaySinh,
                GioiTinh = createStudentDTO.GioiTinh,
                Email = createStudentDTO.Email,
                DiemTB = createStudentDTO.DiemTB,
                LopHocId = createStudentDTO.lopHocId
            };
        }
        // entity -> response
        public static SinhVienResponseDTO ToResponse(this SinhVien student)
        {
            return new SinhVienResponseDTO
            {
                Id = student.Id,
                MaSV = student.MaSV,
                HoTen = student.HoTen,
                NgaySinh = student.NgaySinh,
                GioiTinh = student.GioiTinh,
                Email = student.Email,
                DiemTB = student.DiemTB,
                LopHocId = student.LopHocId
            };
        }
        // update -> entity
        public static void updateEntity(this SinhVien student, SinhVienRequestDTO.SinhVienUpdateDTO updateStudentDTO)
        {

            student.HoTen = updateStudentDTO.HoTen;
            student.NgaySinh = updateStudentDTO.NgaySinh;
            student.GioiTinh = updateStudentDTO.GioiTinh;
            student.Email = updateStudentDTO.Email;
            student.DiemTB = updateStudentDTO.DiemTB;
            student.LopHocId = updateStudentDTO.lopHocId;
        }
        // entity -> response
        public static List<SinhVienResponseDTO> ToResponseList(this IEnumerable<SinhVien> students)
        {
            return students.Select(student => student.ToResponse()).ToList();

        }


    }
}
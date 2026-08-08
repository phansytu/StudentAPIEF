using System.Linq;
using StudentAPIw5.data;
using StudentAPIw5.model;
using static StudentAPIw5.exceptions.StudentException;
using FluentValidation;
namespace StudentAPIw5.validator
{
    public class SinhVienBusinessValidator
    {
        private readonly DataSinhVien _dataSinhVien;

        public SinhVienBusinessValidator(
            DataSinhVien dataSinhVien)
        {
            _dataSinhVien = dataSinhVien;
        }

        public void CheckEmail(string email)
        {
            var exists = _dataSinhVien.SinhViens
                .Any(s => s.Email == email);

            if (exists)
            {
                throw new SinhVienBadRequestException(
                    $"Email {email} đã tồn tại"
                );
            }
        }

        public void CheckEmail(
            string email,
            string id)
        {
            var exists = _dataSinhVien.SinhViens
                .Any(s =>
                    s.Email == email &&
                    s.Id != id);

            if (exists)
            {
                throw new SinhVienBadRequestException(
                    $"Email {email} đã tồn tại"
                );
            }
        }
        public SinhVien CheckMaSv(string masv)
        {
            var student = _dataSinhVien.SinhViens
                .FirstOrDefault(s => s.MaSV == masv);

            if (student == null)
            {
                throw new SinhVienNotFoundException(
                    $"Không tìm thấy sinh viên {masv}"
                );
            }


            return student;
        }

        public SinhVien CheckStudent(string id)
        {
            var student = _dataSinhVien.SinhViens
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                throw new SinhVienNotFoundException(
                    $"Không tìm thấy sinh viên {id}"
                );
            }

            return student;
        }

    }
}
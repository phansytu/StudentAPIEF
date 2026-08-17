using System.Linq;

using StudentAPIw6.Model;
using static StudentAPIw6.Exceptions.StudentException;
using FluentValidation;
using StudentAPIw6.Context;
namespace StudentAPIw6.validator
{
    public class SinhVienBusinessValidator
    {
        private readonly AppDbContext _dataSinhVien;

        public SinhVienBusinessValidator(
            AppDbContext dataSinhVien)
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
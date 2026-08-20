using System.Linq;

using StudentAPIw6.Model;
using static StudentAPIw6.Exceptions.StudentException;
using FluentValidation;
using StudentAPIw6.Context;
namespace StudentAPIw6.validator
{
    public class SinhVienBusinessValidator
    {
        private readonly AppDbContext _appDbContext;

        public SinhVienBusinessValidator(
            AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void CheckEmail(string email)
        {
            var exists = _appDbContext.SinhViens
                .Any(s => s.Email == email);

            if (exists)
            {
                throw new SinhVienBadRequestException(
                    $"Email {email} đã tồn tại"
                );
            }
        }
        public SinhVien CheckIdMsv(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Mã hoặc Id sinh học không được để trống.");
            }
            if (int.TryParse(key, out int id))
            {
                var byId = _appDbContext.SinhViens
                .Find(id);
                if (byId == null)
                {
                    throw new SinhVienNotFoundException($"Lớp học có Id {id} không tồn tại.");
                }
                return byId;
            }
            var ByMa = _appDbContext.SinhViens
                .FirstOrDefault(x => x.MaSV == key);
            if (ByMa == null)
            {
                throw new SinhVienNotFoundException(
                    $"Lớp {key} không tồn tại"
                );
            }
            return ByMa;
        }
    }
}
using System.Linq;

using StudentAPIw6.Model;
using static StudentAPIw6.Exceptions.StudentException;
using FluentValidation;
using StudentAPIw6.Context;
using Microsoft.EntityFrameworkCore;
namespace StudentAPIw6.API.Validators.BusinessValidators
{
    public class SinhVienBusinessValidator
    {
        private readonly AppDbContext _appDbContext;

        public SinhVienBusinessValidator(
            AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CheckEmail(string email)
        {
            var exists = await _appDbContext.SinhViens
                .AnyAsync(s => s.Email == email);

            if (exists)
            {
                throw new SinhVienBadRequestException(
                    $"Email {email} đã tồn tại"
                );
            }
        }
        public async Task<SinhVien> CheckIdMsv(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Mã hoặc Id sinh học không được để trống.");
            }
            if (int.TryParse(key, out int id))
            {
                var byId = await _appDbContext.SinhViens
                .FindAsync(id);
                if (byId == null)
                {
                    throw new SinhVienNotFoundException($"Lớp học có Id {id} không tồn tại.");
                }
                return byId;
            }
            var ByMa = await _appDbContext.SinhViens
                .FirstOrDefaultAsync(x => x.MaSV == key);
            if (ByMa == null)
            {
                throw new SinhVienNotFoundException(
                    $"Lớp {key} không tồn tại"
                );
            }
            return ByMa;
        }
        public async Task<SinhVien> CheckId(int id)
        {
            var byId = await _appDbContext.SinhViens
               .FindAsync(id);
            if (byId == null)
            {
                throw new SinhVienNotFoundException($"Lớp học có Id {id} không tồn tại.");
            }
            return byId;
        }
        public async Task<SinhVien> CheckMsv(string msv)
        {
            var ByMa = await _appDbContext.SinhViens
               .FirstOrDefaultAsync(x => x.MaSV == msv);
            if (ByMa == null)
            {
                throw new SinhVienNotFoundException(
                    $"Lớp {msv} không tồn tại"
                );
            }
            return ByMa;
        }
    }
}
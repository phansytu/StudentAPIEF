using System.Linq;
using StudentAPIw6.Model;
using static StudentAPIw6.API.Exceptions.LopHocException;
using FluentValidation;
using StudentAPIw6.Context;
using Microsoft.EntityFrameworkCore;
namespace StudentAPIw6.API.Validators.BusinessValidators
{
    public class LopHocBusinessValidator
    {
        private readonly AppDbContext _appDbContext;

        public LopHocBusinessValidator(
            AppDbContext appDbContext
            )
        {
            _appDbContext = appDbContext;
        }

        public async Task<LopHoc> CheckIdMaLop(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Mã hoặc Id lớp học không được để trống.");
            }
            if (int.TryParse(key, out int id))
            {
                var byId = await _appDbContext.LopHocs
                .FindAsync(id);
                if (byId == null)
                {
                    throw new LopHocNotFoundException($"Lớp học có Id {id} không tồn tại.");
                }
                return byId;
            }
            var ByMa = await _appDbContext.LopHocs
                .FirstOrDefaultAsync(x => x.MaLop == key);
            if (ByMa == null)
            {
                throw new LopHocNotFoundException(
                    $"Lớp {key} không tồn tại"
                );
            }
            return ByMa;
        }


        public async Task CheckTenLop(string tenLop)
        {
            var exists = await _appDbContext.LopHocs
                .AnyAsync(x => x.TenLop == tenLop);

            if (exists)
            {
                throw new LopHocBadRequestException(
                    $"Tên lớp {tenLop} đã tồn tại"
                );
            }
        }
        // Kiểm tra mã bộ môn có tồn tại không
        public async Task<BoMon> GetBoMonAsync(int MaBM)
        {
            var boMon = await _appDbContext.BoMons
                .FindAsync(MaBM);

            if (boMon == null)
            {
                throw new LopHocBadRequestException(
                    $"Bộ môn '{MaBM}' không tồn tại.");
            }

            return boMon;
        }


    }
}
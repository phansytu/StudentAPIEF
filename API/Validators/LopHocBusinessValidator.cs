using System.Linq;
using StudentAPIw6.Model;
using static StudentAPIw6.Exceptions.LopHocException;
using FluentValidation;
using StudentAPIw6.Context;
namespace StudentAPIw6.validator
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

        public LopHoc CheckIdMaLop(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Mã hoặc Id lớp học không được để trống.");
            }
            if (int.TryParse(key, out int id))
            {
                var byId = _appDbContext.LopHocs
                .Find(id);
                if (byId == null)
                {
                    throw new LopHocNotFoundException($"Lớp học có Id {id} không tồn tại.");
                }
                return byId;
            }
            var ByMa = _appDbContext.LopHocs
                .FirstOrDefault(x => x.MaLop == key);
            if (ByMa == null)
            {
                throw new LopHocNotFoundException(
                    $"Lớp {key} không tồn tại"
                );
            }
            return ByMa;
        }


        public void CheckTenLop(string tenLop)
        {
            var exists = _appDbContext.LopHocs
                .Any(x => x.TenLop == tenLop);

            if (exists)
            {
                throw new LopHocBadRequestException(
                    $"Tên lớp {tenLop} đã tồn tại"
                );
            }
        }


    }
}
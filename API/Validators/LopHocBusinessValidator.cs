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

        public LopHoc CheckMaLop(string maLop)
        {

            var exists = _appDbContext.LopHocs
                .FirstOrDefault(x => x.MaLop == maLop);

            if (exists == null)
            {
                throw new LopHocNotFoundException(
                    $"Lớp {maLop} không tồn tại"
                );
            }
            return exists;
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


        public void CheckTenLop(
            string tenLop,
            string maLop)
        {
            var exists = _appDbContext.LopHocs
                .Any(x =>
                    x.TenLop == tenLop &&
                    x.MaLop != maLop);

            if (exists)
            {
                throw new LopHocBadRequestException(
                    $"Tên lớp {tenLop} đã tồn tại"
                );
            }
        }

        public void CheckCanDelete(string maLop)
        {
            var hasStudent = _appDbContext.SinhViens
                .Any(x => x.MaLop == maLop);

            if (hasStudent)
            {
                throw new LopHocBadRequestException(
                    $"Không thể xóa lớp {maLop} vì lớp đang có sinh viên"
                );
            }
        }
    }
}
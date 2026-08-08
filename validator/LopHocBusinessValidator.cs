using System.Linq;
using StudentAPIw5.data;
using StudentAPIw5.model;
using static StudentAPIw5.exceptions.LopHocException;
using FluentValidation;
namespace StudentAPIw5.validator
{
    public class LopHocBusinessValidator
    {
        private readonly DataLopHoc _dataLopHoc;
        private readonly DataSinhVien _dataSinhVien;

        public LopHocBusinessValidator(
            DataLopHoc dataLopHoc,
            DataSinhVien dataSinhVien)
        {
            _dataLopHoc = dataLopHoc;
            _dataSinhVien = dataSinhVien;
        }

        public LopHoc CheckMaLop(string maLop)
        {

            var exists = _dataLopHoc.LopHocs
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
            var exists = _dataLopHoc.LopHocs
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
            var exists = _dataLopHoc.LopHocs
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
            var hasStudent = _dataSinhVien.SinhViens
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
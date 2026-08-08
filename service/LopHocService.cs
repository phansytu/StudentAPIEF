using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw5.model;
using StudentAPIw5.dto;
using StudentAPIw5.data;
using StudentAPIw5.model.request;
using StudentAPIw5.model.response;
using StudentAPIw5.mapper;
using StudentAPIw5.validator;
namespace StudentAPIw5.service
{
    public class LopHocService : ILopHocService
    {
        private readonly DataLopHoc _dataLopHoc;
        private readonly DataSinhVien _dataSinhVien;
        private readonly LopHocBusinessValidator _business;
        public LopHocService(DataLopHoc dataLopHoc, DataSinhVien dataSinhVien, LopHocBusinessValidator business)
        {
            _dataLopHoc = dataLopHoc;
            _dataSinhVien = dataSinhVien;
            _business = business;
        }
        public void TaoMaLop(LopHoc lophoc)
        {
            int maxSo = 0;

            foreach (var lp in _dataLopHoc.LopHocs)
            {

                if (!string.IsNullOrEmpty(lp.MaLop) &&
                    lp.MaLop.Length > 1 &&
                    int.TryParse(lp.MaLop.Substring(1), out int so))
                {
                    if (so > maxSo)
                    {
                        maxSo = so;
                    }
                }
            }

            int newSo = maxSo + 1;
            lophoc.MaLop = $"L{newSo:D3}";
        }

        public async Task<LopHocDTO.Response> CreateLopHoc(LopHocDTO.LopHocCreateDTO createLopHocDTO)
        {
            _business.CheckTenLop(createLopHocDTO.TenLop);
            //chuyen sang entty
            var lophoc = createLopHocDTO.ToEntity();
            TaoMaLop(lophoc);
            _dataLopHoc.LopHocs.Add(lophoc);
            return lophoc.ToResponse();
        }

        public async Task<bool> DeleteLopHoc(string maLop)
        {
            var lp = _business.CheckMaLop(maLop);
            _business.CheckCanDelete(maLop);
            _dataLopHoc.LopHocs.Remove(lp);
            return true;
        }

        public async Task<PageResponse<LopHocDTO.Response>> GetAllLopHoc(PaginationRequest request)
        {
            var lp = _dataLopHoc.LopHocs.AsQueryable();
            var itemCount = lp.Count();
            var paged = lp.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            var data = LopHocMapper.ToResponseList(paged);

            return new PageResponse<LopHocDTO.Response>
            {
                Data = data,
                TotalCount = itemCount,
                TotalPages = (int)Math.Ceiling((double)itemCount / request.PageSize),
                PageNumber = request.PageNumber
            };

        }

        public async Task<LopHocDTO.Response> GetLopHocById(string maLop)
        {
            var lp = _business.CheckMaLop(maLop);
            return lp.ToResponse();
        }

        public async Task<List<ThongKeLopHoc>> ThongKeLopHoc()
        {
            var result = _dataSinhVien.SinhViens
        .GroupBy(sv => sv.MaLop)
        .Select(group => new ThongKeLopHoc
        {
            MaLop = group.Key,

            SoLuongSinhVien = group.Count(),

            DiemTrungBinh = group.Average(sv => sv.DiemTB),

            DiemCaoNhat = group.Max(sv => sv.DiemTB),

            DiemThapNhat = group.Min(sv => sv.DiemTB)
        })
        .ToList();

            return result;
        }

        public async Task<LopHocDTO.Response> UpdateLopHoc(string maLop, LopHocDTO.LopHocUpdateDTO updateLopHocDTO)
        {
            var lp = _business.CheckMaLop(maLop);
            lp.updateEntity(updateLopHocDTO);
            return lp.ToResponse();
        }


    }
}
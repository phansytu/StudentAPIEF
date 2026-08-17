using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw6.Model;
using StudentAPIw6.DTOs;

using StudentAPIw6.Model.request;
using StudentAPIw6.Model.response;
using StudentAPIw6.AutoMapper;
using StudentAPIw6.validator;
using StudentAPIw6.Context;
namespace StudentAPIw6.Services
{
    public class LopHocService : ILopHocService
    {
        private readonly AppDbContext _appDbContext;

        private readonly LopHocBusinessValidator _business;
        public LopHocService(AppDbContext appDbContext, LopHocBusinessValidator business)
        {
            _appDbContext = appDbContext;
            _business = business;
        }
        public void TaoMaLop(LopHoc lophoc)
        {
            int maxSo = 0;

            foreach (var lp in _appDbContext.LopHocs)
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
            _appDbContext.LopHocs.Add(lophoc);
            return lophoc.ToResponse();
        }

        public async Task<bool> DeleteLopHoc(string maLop)
        {
            var lp = _business.CheckMaLop(maLop);
            _business.CheckCanDelete(maLop);
            _appDbContext.LopHocs.Remove(lp);
            return true;
        }

        public async Task<PageResponse<LopHocDTO.Response>> GetAllLopHoc(PaginationRequest request)
        {
            var lp = _appDbContext.LopHocs.AsQueryable();
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
            var result = _appDbContext.SinhViens
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
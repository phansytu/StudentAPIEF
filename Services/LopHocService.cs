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
using Microsoft.EntityFrameworkCore;
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
        public async Task<LopHocDTO.Response> CreateLopHoc(LopHocDTO.LopHocCreateDTO createLopHocDTO)
        {

            await _business.CheckTenLop(createLopHocDTO.TenLop);
            var boMon = await _business.GetBoMonAsync(createLopHocDTO.boMonId);
            //chuyen sang entty
            var lophoc = createLopHocDTO.ToEntity();

            lophoc.BoMonId = boMon.id;

            _appDbContext.LopHocs.Add(lophoc);
            await _appDbContext.SaveChangesAsync();
            return lophoc.ToResponse();
        }

        public async Task<bool> DeleteLopHoc(string key)
        {
            var lp = await _business.CheckIdMaLop(key);
            _appDbContext.LopHocs.Remove(lp);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PageResponse<LopHocDTO.Response>> GetAllLopHoc(PaginationRequest request)
        {
            var lp = _appDbContext.LopHocs.AsQueryable();
            var itemCount = await lp.CountAsync();
            var paged = lp.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

            var pagedList = await paged.ToListAsync();
            var data = LopHocMapper.ToResponseList(pagedList);

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
            var lp = await _business.CheckIdMaLop(maLop);
            return lp.ToResponse();
        }

        public async Task<List<ThongKeLopHoc>> ThongKeLopHoc()
        {
            var result = await _appDbContext.SinhViens
        .GroupBy(sv => sv.LopHocId)
        .Select(group => new ThongKeLopHoc
        {
            lopHocId = group.Key,

            SoLuongSinhVien = group.Count(),

            DiemTrungBinh = group.Average(sv => sv.DiemTB),

            DiemCaoNhat = group.Max(sv => sv.DiemTB),

            DiemThapNhat = group.Min(sv => sv.DiemTB)
        })
        .ToListAsync();

            return result;
        }

        public async Task<LopHocDTO.Response> UpdateLopHoc(string key, LopHocDTO.LopHocUpdateDTO updateLopHocDTO)
        {
            var lp = await _business.CheckIdMaLop(key);
            lp.updateEntity(updateLopHocDTO);
            return lp.ToResponse();
        }


    }
}
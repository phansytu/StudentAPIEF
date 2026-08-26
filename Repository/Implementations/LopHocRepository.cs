using Microsoft.EntityFrameworkCore;
using StudentAPIw6.Model;
using StudentAPIw6.Context;
using StudentAPIw6.API.DTOs.response;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.Repository.Interfaces;

namespace StudentAPIw6.Repository.Implementations
{
    public class LopHocRepository : ILopHocRepository
    {
        private readonly AppDbContext _appDbContext;

        public LopHocRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<(List<LopHoc> Data, int TotalCount)> GetAllAsync(PaginationRequest request)
        {
            var query = _appDbContext.LopHocs.AsNoTracking().AsQueryable();

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<LopHoc?> GetByMaLopAsync(string maLop)
            => await _appDbContext.LopHocs.AsNoTracking().FirstOrDefaultAsync(x => x.MaLop == maLop);

        public async Task<LopHoc?> GetByTenLopAsync(string tenLop)
            => await _appDbContext.LopHocs.AsNoTracking().FirstOrDefaultAsync(x => x.TenLop == tenLop);

        public async Task AddAsync(LopHoc lopHoc)
            => await _appDbContext.LopHocs.AddAsync(lopHoc);

        public Task UpdateAsync(LopHoc lopHoc)
        {
            _appDbContext.LopHocs.Update(lopHoc);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(LopHoc lopHoc)
        {
            _appDbContext.LopHocs.Remove(lopHoc);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
            => await _appDbContext.SaveChangesAsync() > 0;

        public async Task<List<ThongKeLopHoc>> ThongKeLopHocAsync()
        {
            var query =
                from sv in _appDbContext.SinhViens.AsNoTracking()

                join lp in _appDbContext.LopHocs on sv.LopHocId equals lp.Id
                // join bm in _appDbContext.BoMons on lp.BoMonId equals bm.Id
                // group sv by sv.LopHocId into g
                group new { sv, lp } by new { lp.Id, lp.TenLop, lp.ChuyenNganh } into g
                select new ThongKeLopHoc
                {
                    lopHocId = g.Key.Id,
                    TenLop = g.Key.TenLop,
                    ChuyenNganh = g.Key.ChuyenNganh,
                    // TenBoMon = bm.TenBoMon,
                    SoLuongSinhVien = g.Count(),
                    DiemTrungBinh = g.Average(x => x.sv.DiemTB),
                    DiemCaoNhat = g.Max(x => x.sv.DiemTB),
                    DiemThapNhat = g.Min(x => x.sv.DiemTB)
                };

            return await query.ToListAsync();
        }
    }
}
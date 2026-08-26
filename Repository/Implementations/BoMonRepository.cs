using Microsoft.EntityFrameworkCore;
using StudentAPIw6.Model;
using StudentAPIw6.Context;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.Repository.Interfaces;

namespace StudentAPIw6.Repository.Implementations
{
    public class BoMonRepository : IBoMonRepository
    {
        private readonly AppDbContext _appDbContext;

        public BoMonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<(List<BoMon> Data, int TotalCount)> GetAllAsync(PaginationRequest request)
        {
            var query = _appDbContext.BoMons.AsNoTracking().AsQueryable();

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<BoMon?> GetByIdAsync(int Id)
            => await _appDbContext.BoMons.AsNoTracking().FirstOrDefaultAsync(x => x.id == Id);

        public async Task<BoMon?> GetByMaBoMonAsync(string maBoMon)
            => await _appDbContext.BoMons.AsNoTracking().FirstOrDefaultAsync(x => x.maBM == maBoMon);

        public async Task<BoMon?> GetByTenBoMonAsync(string tenBoMon)
            => await _appDbContext.BoMons.AsNoTracking().FirstOrDefaultAsync(x => x.tenBM == tenBoMon);

        public async Task AddAsync(BoMon boMon)
            => await _appDbContext.BoMons.AddAsync(boMon);

        public Task UpdateAsync(BoMon boMon)
        {
            _appDbContext.BoMons.Update(boMon);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(BoMon boMon)
        {
            _appDbContext.BoMons.Remove(boMon);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
            => await _appDbContext.SaveChangesAsync() > 0;
    }
}
using StudentAPIw6.Model;
using StudentAPIw6.API.DTOs.Request;

namespace StudentAPIw6.Repository
{
    public interface IBoMonRepository
    {
        Task<(List<BoMon> Data, int TotalCount)> GetAllAsync(PaginationRequest request);
        Task<BoMon?> GetByIdAsync(int id);
        Task<BoMon?> GetByMaBoMonAsync(string maBoMon);
        Task<BoMon?> GetByTenBoMonAsync(string tenBoMon);
        Task AddAsync(BoMon boMon);
        Task UpdateAsync(BoMon boMon);
        Task DeleteAsync(BoMon boMon);
        Task<bool> SaveChangesAsync();
    }
}
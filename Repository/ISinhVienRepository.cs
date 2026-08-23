using StudentAPIw6.Model;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.Common.Wrappers;

namespace StudentAPIw6.Services
{
    public interface ISinhVienRepository
    {

        Task<(List<SinhVien> Data, int TotalCount)> GetAllAsync(SinhVienQueryRequest request);
        Task<SinhVien?> GetByIdAsync(int id);
        Task<SinhVien?> GetByMsvAsync(string maSV);
        Task AddAsync(SinhVien student);
        Task UpdateAsync(SinhVien student);
        Task DeleteAsync(SinhVien student);
        Task<bool> SaveChangesAsync();
        Task<PageResponse<SinhVienAdvancedDTO>> GetPagedAdvancedAsync(SinhVienAdvancedRequest request);
    }
}
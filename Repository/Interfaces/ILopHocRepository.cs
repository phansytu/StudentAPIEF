
using StudentAPIw6.Model;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.DTOs.response;
namespace StudentAPIw6.Repository.Interfaces
{
    public interface ILopHocRepository
    {
        Task<(List<LopHoc> Data, int TotalCount)> GetAllAsync(PaginationRequest request);
        Task<LopHoc?> GetByMaLopAsync(string maLop);
        Task<LopHoc?> GetByTenLopAsync(string tenLop);
        Task AddAsync(LopHoc lopHoc);
        Task UpdateAsync(LopHoc lopHoc);
        Task DeleteAsync(LopHoc lopHoc);
        Task<bool> SaveChangesAsync();

        // Thống kê - join sang SinhVien
        Task<List<ThongKeLopHoc>> ThongKeLopHocAsync();
    }
}
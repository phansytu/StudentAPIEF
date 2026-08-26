using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.Common.Wrappers;

namespace StudentAPIw6.Repository.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardSummaryDTO> GetSummaryStatsAsync();
        Task<PageResponse<BaoCaoChiTietSinhVienDTO>> GetBaoCaoChiTietSinhVienAsync(BaoCaoChiTietRequest request);
        Task<List<BaoCaoThongKeTheoLopDTO>> GetBaoCaoThongKeTheoLopAsync();
    }
}
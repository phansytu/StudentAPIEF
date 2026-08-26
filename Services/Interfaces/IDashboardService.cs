using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.Common.Wrappers;

namespace StudentAPIw6.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDTO> GetSummaryStats();
        Task<PageResponse<BaoCaoChiTietSinhVienDTO>> GetBaoCaoChiTietSinhVien(BaoCaoChiTietRequest request);
        Task<List<BaoCaoThongKeTheoLopDTO>> GetBaoCaoThongKeTheoLop();
    }
}
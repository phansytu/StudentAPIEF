using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.Common.Wrappers;
using StudentAPIw6.Repository;

namespace StudentAPIw6.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardSummaryDTO> GetSummaryStats()
            => await _repository.GetSummaryStatsAsync();

        public async Task<PageResponse<BaoCaoChiTietSinhVienDTO>> GetBaoCaoChiTietSinhVien(BaoCaoChiTietRequest request)
        {
            if (request.PageIndex < 1) request.PageIndex = 1;
            if (request.PageSize < 1) request.PageSize = 10;

            return await _repository.GetBaoCaoChiTietSinhVienAsync(request);
        }

        public async Task<List<BaoCaoThongKeTheoLopDTO>> GetBaoCaoThongKeTheoLop()
            => await _repository.GetBaoCaoThongKeTheoLopAsync();
    }
}
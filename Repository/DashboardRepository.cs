using System.Data;
using Microsoft.Data.SqlClient;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.Common.Wrappers;

namespace StudentAPIw6.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly string _connectionString;

        public DashboardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // sp_Dashboard_GetSummaryStats
        public async Task<DashboardSummaryDTO> GetSummaryStatsAsync()
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand("sp_Dashboard_GetSummaryStats", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            DashboardSummaryDTO result;

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (!await reader.ReadAsync())
                    return new DashboardSummaryDTO();

                result = new DashboardSummaryDTO
                {
                    TongSoSinhVien = reader.GetInt32(reader.GetOrdinal("TongSoSinhVien")),
                    TongSoLop = reader.GetInt32(reader.GetOrdinal("TongSoLop")),
                    TongSoBoMon = reader.GetInt32(reader.GetOrdinal("TongSoBoMon")),
                    DiemTrungBinhToanTruong = Convert.ToDouble(reader["DiemTrungBinhToanTruong"]),
                    SoSinhVienGioi = reader.GetInt32(reader.GetOrdinal("SoSinhVienGioi"))
                };
            }

            return result;
        }

        // vw_BaoCao_ChiTietSinhVien (có filter + paging)
        public async Task<PageResponse<BaoCaoChiTietSinhVienDTO>> GetBaoCaoChiTietSinhVienAsync(BaoCaoChiTietRequest request)
        {
            var response = new PageResponse<BaoCaoChiTietSinhVienDTO>
            {
                PageNumber = request.PageIndex,
                PageSize = request.PageSize
            };

            var sql = @"
                SELECT *, COUNT(*) OVER() AS TotalCount
                FROM dbo.vw_BaoCao_ChiTietSinhVien
                WHERE (@Keyword IS NULL OR HoTen LIKE N'%' + @Keyword + '%' OR MaSinhVien LIKE '%' + @Keyword + '%')
                  AND (@LopHocId IS NULL OR LopHocId = @LopHocId)
                  AND (@BoMonId IS NULL OR BoMonId = @BoMonId)
                ORDER BY SinhVienId DESC
                OFFSET (@PageIndex - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Keyword", (object?)request.Keyword ?? DBNull.Value);
            command.Parameters.AddWithValue("@LopHocId", (object?)request.LopHocId ?? DBNull.Value);
            command.Parameters.AddWithValue("@BoMonId", (object?)request.BoMonId ?? DBNull.Value);
            command.Parameters.AddWithValue("@PageIndex", request.PageIndex);
            command.Parameters.AddWithValue("@PageSize", request.PageSize);

            await connection.OpenAsync();

            int totalCount = 0;

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(reader.GetOrdinal("TotalCount"));

                    response.Data.Add(new BaoCaoChiTietSinhVienDTO
                    {
                        SinhVienId = reader.GetInt32(reader.GetOrdinal("SinhVienId")),
                        MaSinhVien = reader.GetString(reader.GetOrdinal("MaSinhVien")),
                        HoTen = reader.GetString(reader.GetOrdinal("HoTen")),
                        GioiTinh = reader.GetBoolean(reader.GetOrdinal("GioiTinh")),
                        GioiTinhText = reader.GetString(reader.GetOrdinal("GioiTinhText")),
                        NgaySinh = reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                        Tuoi = reader.GetInt32(reader.GetOrdinal("Tuoi")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        DiemTB = Convert.ToDouble(reader["DiemTB"]),
                        XepLoai = reader.GetString(reader.GetOrdinal("XepLoai")),
                        LopHocId = reader.IsDBNull(reader.GetOrdinal("LopHocId")) ? null : reader.GetInt32(reader.GetOrdinal("LopHocId")),
                        MaLop = reader.GetString(reader.GetOrdinal("MaLop")),
                        TenLop = reader.GetString(reader.GetOrdinal("TenLop")),
                        ChuyenNganh = reader.GetString(reader.GetOrdinal("ChuyenNganh")),
                        BoMonId = reader.IsDBNull(reader.GetOrdinal("BoMonId")) ? null : reader.GetInt32(reader.GetOrdinal("BoMonId")),
                        TenBoMon = reader.GetString(reader.GetOrdinal("TenBoMon"))
                    });
                }
            }

            response.TotalCount = totalCount;
            response.TotalPages = request.PageSize > 0
                ? (int)Math.Ceiling((double)totalCount / request.PageSize)
                : 0;

            return response;
        }

        // vw_BaoCao_ThongKeTheoLop
        public async Task<List<BaoCaoThongKeTheoLopDTO>> GetBaoCaoThongKeTheoLopAsync()
        {
            var result = new List<BaoCaoThongKeTheoLopDTO>();

            var sql = "SELECT * FROM dbo.vw_BaoCao_ThongKeTheoLop ORDER BY LopHocId;";

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            await connection.OpenAsync();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Add(new BaoCaoThongKeTheoLopDTO
                    {
                        LopHocId = reader.GetInt32(reader.GetOrdinal("LopHocId")),
                        MaLop = reader.GetString(reader.GetOrdinal("MaLop")),
                        TenLop = reader.GetString(reader.GetOrdinal("TenLop")),
                        ChuyenNganh = reader.GetString(reader.GetOrdinal("ChuyenNganh")),
                        TenBoMon = reader.GetString(reader.GetOrdinal("TenBoMon")),
                        TongSoSinhVien = reader.GetInt32(reader.GetOrdinal("TongSoSinhVien")),
                        SoNam = reader.GetInt32(reader.GetOrdinal("SoNam")),
                        SoNu = reader.GetInt32(reader.GetOrdinal("SoNu")),
                        DiemTrungBinhLop = Convert.ToDouble(reader["DiemTrungBinhLop"]),
                        DiemCaoNhat = Convert.ToDouble(reader["DiemCaoNhat"]),
                        DiemThapNhat = Convert.ToDouble(reader["DiemThapNhat"])
                    });
                }
            }

            return result;
        }
    }
}
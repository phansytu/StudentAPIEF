using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using StudentAPIw6.Model;
using StudentAPIw6.Context;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.Model.response;
using StudentAPIw6.Model.request;
using StudentAPIw6.Common.Wrappers;

namespace StudentAPIw6.Services
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly string _connectionString;

        public SinhVienRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }


        public async Task<(List<SinhVien> Data, int TotalCount)> GetAllAsync(SinhVienQueryRequest request)
        {
            var query = _appDbContext.SinhViens.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(x =>
                    x.MaSV.Contains(request.Keyword) ||
                    x.HoTen.Contains(request.Keyword) ||
                    x.Email.Contains(request.Keyword));
            }

            if (request.GioiTinh.HasValue)
                query = query.Where(x => x.GioiTinh == request.GioiTinh.Value);

            if (request.DiemTu.HasValue)
                query = query.Where(x => x.DiemTB >= request.DiemTu.Value);

            if (request.DiemDen.HasValue)
                query = query.Where(x => x.DiemTB <= request.DiemDen.Value);

            query = request.SortBy?.ToLower() switch
            {
                "hoten" => request.Descending
                    ? query.OrderByDescending(x => x.HoTen)
                    : query.OrderBy(x => x.HoTen),
                "diemtb" => request.Descending
                    ? query.OrderByDescending(x => x.DiemTB)
                    : query.OrderBy(x => x.DiemTB),
                _ => query
            };

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<SinhVien?> GetByIdAsync(int id)
            => await _appDbContext.SinhViens.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<SinhVien?> GetByMsvAsync(string maSV)
            => await _appDbContext.SinhViens.FirstOrDefaultAsync(x => x.MaSV == maSV);

        public async Task AddAsync(SinhVien student)
            => await _appDbContext.SinhViens.AddAsync(student);

        public Task UpdateAsync(SinhVien student)
        {
            _appDbContext.SinhViens.Update(student);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(SinhVien student)
        {
            _appDbContext.SinhViens.Remove(student);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
            => await _appDbContext.SaveChangesAsync() > 0;



        public async Task<PageResponse<SinhVienAdvancedDTO>> GetPagedAdvancedAsync(SinhVienAdvancedRequest request)
        {
            var response = new PageResponse<SinhVienAdvancedDTO>
            {
                PageNumber = request.PageIndex,
                PageSize = request.PageSize
            };

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand("sp_SinhVien_GetPagedAdvanced", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PageIndex", request.PageIndex);
            command.Parameters.AddWithValue("@PageSize", request.PageSize);
            command.Parameters.AddWithValue("@Keyword", (object?)request.Keyword ?? DBNull.Value);
            command.Parameters.AddWithValue("@LopHocId", (object?)request.LopHocId ?? DBNull.Value);
            command.Parameters.AddWithValue("@BoMonId", (object?)request.BoMonId ?? DBNull.Value);
            command.Parameters.AddWithValue("@MinDiem", (object?)request.MinDiem ?? DBNull.Value);
            command.Parameters.AddWithValue("@MaxDiem", (object?)request.MaxDiem ?? DBNull.Value);

            var totalRecordsParameter = new SqlParameter("@TotalRecords", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(totalRecordsParameter);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                response.Data.Add(new SinhVienAdvancedDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Msv = reader.GetString(reader.GetOrdinal("msv")),
                    HoTen = reader.GetString(reader.GetOrdinal("hoTen")),
                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("gioiTinh"))
                        ? false
                        : reader.GetBoolean(reader.GetOrdinal("gioiTinh")),
                    NgaySinh = reader.GetDateTime(reader.GetOrdinal("ngaySinh")),
                    Email = reader.IsDBNull(reader.GetOrdinal("email"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("email")),
                    DiemTb = reader.GetDecimal(reader.GetOrdinal("diemTb")),
                    TenLop = reader.GetString(reader.GetOrdinal("tenLop")),
                    TenMon = reader.GetString(reader.GetOrdinal("tenMon"))
                });
            }

            response.TotalPages = (int)totalRecordsParameter.Value;

            return response;
        }
    }
}
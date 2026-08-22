using StudentAPIw6.Model.request;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.Common.Wrappers;
namespace StudentAPIw6.Services
{
    public interface ISinhVienService
    {
        Task<PageResponse<SinhVienResponseDTO>> GetAll(SinhVienQueryRequest request);

        //getbyid
        Task<SinhVienResponseDTO> GetSinhVienByMsv(string masv);
        Task<SinhVienResponseDTO> GetSinhVienById(int id);
        //create
        Task<SinhVienResponseDTO> CreateSinhVien(SinhVienRequestDTO.SinhVienCreateDTO createStudentDTO);
        //update
        Task<SinhVienResponseDTO> UpdateSinhVien(string maSV, SinhVienRequestDTO.SinhVienUpdateDTO updateStudentDTO);
        //delete
        Task<bool> DeleteSinhVien(string maSV);
        Task<PageResponse<SinhVienAdvancedDTO>> GetPagedAdvancedAsync(SinhVienAdvancedRequest request);

    }
}
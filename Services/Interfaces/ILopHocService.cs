
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.Common.Wrappers;
using StudentAPIw6.API.DTOs.response;
namespace StudentAPIw6.Services.Interfaces
{
    public interface ILopHocService
    {
        Task<PageResponse<LopHocResponseDTO>> GetAllLopHoc(PaginationRequest request);
        Task<LopHocResponseDTO> GetLopHocById(string lopHocId);
        Task<LopHocResponseDTO> CreateLopHoc(LopHocRequestDTO.LopHocCreateDTO createLopHocDTO);
        Task<LopHocResponseDTO> UpdateLopHoc(string key, LopHocRequestDTO.LopHocUpdateDTO updateLopHocDTO);
        Task<bool> DeleteLopHoc(string maLop);
        Task<List<ThongKeLopHoc>> ThongKeLopHoc();
    }
}
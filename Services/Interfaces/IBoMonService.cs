using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.Common.Wrappers;


namespace StudentAPIw6.Services.Interfaces
{
    public interface IBoMonService
    {
        Task<PageResponse<BoMonResponseDTO>> GetAllBoMon(PaginationRequest request);
        Task<BoMonResponseDTO> GetBoMonByMa(string maBoMon);
        Task<BoMonResponseDTO> CreateBoMon(BoMonRequestDTO.CreateBoMonDTO createDTO);
        Task<BoMonResponseDTO> UpdateBoMon(string maBoMon, BoMonRequestDTO.UpdateBoMonDTO updateDTO);
        Task<bool> DeleteBoMon(string maBoMon);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw6.Model;
using StudentAPIw6.Model.request;
using StudentAPIw6.Model.response;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.Common.Wrappers;
namespace StudentAPIw6.Services
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
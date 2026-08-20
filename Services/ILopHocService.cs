using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw6.Model;
using StudentAPIw6.DTOs;
using StudentAPIw6.Model.request;
using StudentAPIw6.Model.response;
namespace StudentAPIw6.Services
{
    public interface ILopHocService
    {
        Task<PageResponse<LopHocDTO.Response>> GetAllLopHoc(PaginationRequest request);
        Task<LopHocDTO.Response> GetLopHocById(string lopHocId);
        Task<LopHocDTO.Response> CreateLopHoc(LopHocDTO.LopHocCreateDTO createLopHocDTO);
        Task<LopHocDTO.Response> UpdateLopHoc(string key, LopHocDTO.LopHocUpdateDTO updateLopHocDTO);
        Task<bool> DeleteLopHoc(string maLop);
        Task<List<ThongKeLopHoc>> ThongKeLopHoc();
    }
}
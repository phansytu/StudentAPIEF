using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw5.model;
using StudentAPIw5.dto;
using StudentAPIw5.model.request;
using StudentAPIw5.model.response;
namespace StudentAPIw5.service
{
    public interface ILopHocService
    {
        Task<PageResponse<LopHocDTO.Response>> GetAllLopHoc(PaginationRequest request);
        Task<LopHocDTO.Response> GetLopHocById(string maLop);
        Task<LopHocDTO.Response> CreateLopHoc(LopHocDTO.LopHocCreateDTO createLopHocDTO);
        Task<LopHocDTO.Response> UpdateLopHoc(string maLop, LopHocDTO.LopHocUpdateDTO updateLopHocDTO);
        Task<bool> DeleteLopHoc(string maLop);
        void TaoMaLop(LopHoc lopHoc);
        Task<List<ThongKeLopHoc>> ThongKeLopHoc();
    }
}
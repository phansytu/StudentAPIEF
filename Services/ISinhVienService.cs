using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw6.Model;
using StudentAPIw6.DTOs;
using StudentAPIw6.Model.response;
using StudentAPIw6.Model.request;
namespace StudentAPIw6.Services
{
    public interface ISinhVienService
    {
        Task<PageResponse<SinhVienDTO.Response>> GetAll(SinhVienQueryRequest request);

        //getbyid
        Task<SinhVienDTO.Response> GetSinhVienById(string key);
        //create
        Task<SinhVienDTO.Response> CreateSinhVien(SinhVienDTO.SinhVienCreateDTO createStudentDTO);
        //update
        Task<SinhVienDTO.Response> UpdateSinhVien(string maSV, SinhVienDTO.SinhVienUpdateDTO updateStudentDTO);
        //delete
        Task<bool> DeleteSinhVien(string maSV);

    }
}
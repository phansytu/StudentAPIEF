using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw5.model;
using StudentAPIw5.dto;
using StudentAPIw5.model.response;
using StudentAPIw5.model.request;
namespace StudentAPIw5.service
{
    public interface ISinhVienService
    {
        Task<PageResponse<SinhVienDTO.Response>> GetAll(SinhVienQueryRequest request);

        //getbyid
        Task<SinhVienDTO.Response> GetSinhVienById(string id);
        //create
        Task<SinhVienDTO.Response> CreateSinhVien(SinhVienDTO.SinhVienCreateDTO createStudentDTO);
        //update
        Task<SinhVienDTO.Response> UpdateSinhVien(string maSV, SinhVienDTO.SinhVienUpdateDTO updateStudentDTO);
        //delete
        Task<bool> DeleteSinhVien(string maSV);
        public void TaoMaIdSinhVienTuDong(SinhVien sinhVien);

    }
}
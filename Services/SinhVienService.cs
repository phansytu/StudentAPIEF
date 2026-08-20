using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw6.Model;
using StudentAPIw6.DTOs;
using StudentAPIw6.Model.response;
using StudentAPIw6.Model.request;
using StudentAPIw6.Services;
using StudentAPIw6.AutoMapper;

using StudentAPIw6.validator;
using StudentAPIw6.Context;
namespace StudentAPIw6.Services
{
    public class SinhVienService : ISinhVienService
    {
        public readonly AppDbContext _appDbContext;
        private readonly SinhVienBusinessValidator _businessValidator;
        public SinhVienService(AppDbContext dataSinhVien, SinhVienBusinessValidator businessValidator)
        {
            _appDbContext = dataSinhVien;
            _businessValidator = businessValidator;
        }

        public async Task<PageResponse<SinhVienDTO.Response>> GetAll(SinhVienQueryRequest request)
        {
            var students = _appDbContext.SinhViens.AsQueryable();
            //tim kiem
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                students = students.Where(x =>
                x.MaSV.Contains(request.Keyword) ||
                x.HoTen.Contains(request.Keyword) ||
                x.Email.Contains(request.Keyword));
            }
            //loc gioi tinh
            if (request.GioiTinh.HasValue)
            {
                students = students.Where(x => x.GioiTinh == request.GioiTinh.Value);
            }
            //loc diem
            if (request.DiemTu.HasValue)
            {
                students = students.Where(x => x.DiemTB >= request.DiemTu.Value);
            }
            if (request.DiemDen.HasValue)
                students = students.Where(x => x.DiemTB <= request.DiemDen.Value);
            //sap xep
            students = request.SortBy?.ToLower() switch
            {
                "hoten" => request.Descending ? students.OrderByDescending(x => x.HoTen) : students.OrderBy(x => x.HoTen),
                "diemtb" => request.Descending ? students.OrderByDescending(x => x.DiemTB) : students.OrderBy(x => x.DiemTB),
                _ => students
            };

            var itemCount = students.Count();
            var paged = students.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            var data = StudentMapper.ToResponseList(paged);

            return new PageResponse<SinhVienDTO.Response>
            {
                Data = data,
                TotalCount = itemCount,
                TotalPages = (int)Math.Ceiling((double)itemCount / request.PageSize),
                PageNumber = request.PageNumber
            };
        }


        public async Task<SinhVienDTO.Response> GetSinhVienById(string key)
        {
            var sv = _businessValidator.CheckIdMsv(key);
            return sv.ToResponse();
        }
        public async Task<SinhVienDTO.Response> CreateSinhVien(SinhVienDTO.SinhVienCreateDTO createStudentDTO)
        {
            _businessValidator.CheckEmail(createStudentDTO.Email);
            var student = createStudentDTO.ToEntity();
            _appDbContext.SinhViens.Add(student);
            return student.ToResponse();
        }
        public async Task<SinhVienDTO.Response> UpdateSinhVien(string maSV, SinhVienDTO.SinhVienUpdateDTO updateStudentDTO)
        {
            var sv = _businessValidator.CheckIdMsv(maSV);
            sv.updateEntity(updateStudentDTO);
            return sv.ToResponse();
        }

        public async Task<bool> DeleteSinhVien(string maSV)
        {
            var sv = _businessValidator.CheckIdMsv(maSV);
            _appDbContext.SinhViens.Remove(sv);
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw5.model;
using StudentAPIw5.dto;
using StudentAPIw5.model.response;
using StudentAPIw5.model.request;
using StudentAPIw5.service;
using StudentAPIw5.mapper;
using StudentAPIw5.data;
using StudentAPIw5.validator;
namespace StudentAPIw5.service
{
    public class SinhVienService : ISinhVienService
    {
        public readonly DataSinhVien _dataSinhVien;
        private readonly SinhVienBusinessValidator _businessValidator;
        public SinhVienService(DataSinhVien dataSinhVien, SinhVienBusinessValidator businessValidator)
        {
            _dataSinhVien = dataSinhVien;
            _businessValidator = businessValidator;
        }

        public void TaoMaIdSinhVienTuDong(SinhVien sinhVien)
        {
            int maxSo = 0;

            foreach (var sv in _dataSinhVien.SinhViens)
            {

                if (!string.IsNullOrEmpty(sv.MaSV) &&
                    sv.MaSV.Length > 3 &&
                    int.TryParse(sv.MaSV.Substring(3), out int so))
                {
                    if (so > maxSo)
                    {
                        maxSo = so;
                    }
                }
            }

            int newSo = maxSo + 1;
            sinhVien.Id = $"SV{newSo:D4}";
            sinhVien.MaSV = $"MSV{newSo:D4}";
        }

        public async Task<PageResponse<SinhVienDTO.Response>> GetAll(SinhVienQueryRequest request)
        {
            var students = _dataSinhVien.SinhViens.AsQueryable();
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


        public async Task<SinhVienDTO.Response> GetSinhVienById(string id)
        {
            var sv = _businessValidator.CheckStudent(id);
            return sv.ToResponse();
        }
        public async Task<SinhVienDTO.Response> CreateSinhVien(SinhVienDTO.SinhVienCreateDTO createStudentDTO)
        {
            _businessValidator.CheckEmail(createStudentDTO.Email);
            var student = createStudentDTO.ToEntity();
            TaoMaIdSinhVienTuDong(student);
            _dataSinhVien.SinhViens.Add(student);
            return student.ToResponse();
        }
        public async Task<SinhVienDTO.Response> UpdateSinhVien(string maSV, SinhVienDTO.SinhVienUpdateDTO updateStudentDTO)
        {
            var sv = _businessValidator.CheckMaSv(maSV);
            _businessValidator.CheckEmail(updateStudentDTO.Email, sv.Id);
            sv.updateEntity(updateStudentDTO);
            return sv.ToResponse();
        }

        public async Task<bool> DeleteSinhVien(string maSV)
        {
            var sv = _businessValidator.CheckMaSv(maSV);
            _dataSinhVien.SinhViens.Remove(sv);
            return true;
        }
    }
}
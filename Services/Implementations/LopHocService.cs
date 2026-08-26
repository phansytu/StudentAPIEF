using System;
using StudentAPIw6.AutoMapper;
using StudentAPIw6.Context;
using Microsoft.EntityFrameworkCore;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.Validators.BusinessValidators;
using StudentAPIw6.Common.Wrappers;
using StudentAPIw6.Repository.Interfaces;
using StudentAPIw6.API.DTOs.response;
using StudentAPIw6.Services.Interfaces;
namespace StudentAPIw6.Services.Implementations
{
    public class LopHocService : ILopHocService
    {
        private readonly ILopHocRepository _repository;

        private readonly LopHocBusinessValidator _business;
        public LopHocService(ILopHocRepository repository, LopHocBusinessValidator business)
        {
            _repository = repository;
            _business = business;
        }
        public async Task<LopHocResponseDTO> CreateLopHoc(LopHocRequestDTO.LopHocCreateDTO createLopHocDTO)
        {

            await _business.CheckTenLop(createLopHocDTO.TenLop);
            var boMon = await _business.GetBoMonAsync(createLopHocDTO.boMonId);
            //chuyen sang entty
            var lophoc = createLopHocDTO.ToEntity();

            lophoc.BoMonId = boMon.id;

            await _repository.AddAsync(lophoc);
            await _repository.SaveChangesAsync();
            return lophoc.ToResponse();
        }

        public async Task<bool> DeleteLopHoc(string key)
        {
            var lp = await _business.CheckIdMaLop(key);
            await _repository.DeleteAsync(lp);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<PageResponse<LopHocResponseDTO>> GetAllLopHoc(PaginationRequest request)
        {
            var (data, totalCount) = await _repository.GetAllAsync(request);

            return new PageResponse<LopHocResponseDTO>
            {
                Data = LopHocMapper.ToResponseList(data),
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
                PageNumber = request.PageNumber
            };

        }

        public async Task<LopHocResponseDTO> GetLopHocById(string maLop)
        {
            var lp = await _business.CheckIdMaLop(maLop);
            return lp.ToResponse();
        }

        public async Task<List<ThongKeLopHoc>> ThongKeLopHoc()
        {
            var result = await _repository.ThongKeLopHocAsync();
            return result;
        }

        public async Task<LopHocResponseDTO> UpdateLopHoc(string key, LopHocRequestDTO.LopHocUpdateDTO updateLopHocDTO)
        {
            var lp = await _business.CheckIdMaLop(key);
            lp.updateEntity(updateLopHocDTO);
            return lp.ToResponse();
        }
    }


}
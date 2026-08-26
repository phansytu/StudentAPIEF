using System;
using StudentAPIw6.AutoMapper;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.Validators.BusinessValidators;
using StudentAPIw6.Common.Wrappers;
using StudentAPIw6.Repository.Interfaces;
using StudentAPIw6.Services.Interfaces;

namespace StudentAPIw6.Services.Implementations
{
    public class BoMonService : IBoMonService
    {
        private readonly IBoMonRepository _repository;
        private readonly BoMonBusinessValidator _business;

        public BoMonService(IBoMonRepository repository, BoMonBusinessValidator business)
        {
            _repository = repository;
            _business = business;
        }

        public async Task<BoMonResponseDTO> CreateBoMon(BoMonRequestDTO.CreateBoMonDTO createDTO)
        {
            await _business.CheckTenBoMon(createDTO.tenMon);

            var boMon = createDTO.ToEntity();

            await _repository.AddAsync(boMon);
            await _repository.SaveChangesAsync();

            return boMon.ToResponse();
        }

        public async Task<bool> DeleteBoMon(string maBoMon)
        {
            var bm = await _business.CheckMaBM(maBoMon);
            await _repository.DeleteAsync(bm);
            return await _repository.SaveChangesAsync();
        }

        public async Task<PageResponse<BoMonResponseDTO>> GetAllBoMon(PaginationRequest request)
        {
            var (data, totalCount) = await _repository.GetAllAsync(request);

            return new PageResponse<BoMonResponseDTO>
            {
                Data = BoMonMapper.ToResponseList(data),
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
                PageNumber = request.PageNumber
            };
        }

        public async Task<BoMonResponseDTO> GetBoMonByMa(string maBoMon)
        {
            var bm = await _business.CheckMaBM(maBoMon);
            return bm.ToResponse();
        }

        public async Task<BoMonResponseDTO> UpdateBoMon(string maBoMon, BoMonRequestDTO.UpdateBoMonDTO updateDTO)
        {
            var bm = await _business.CheckMaBM(maBoMon);
            bm.updateEntity(updateDTO);

            await _repository.UpdateAsync(bm);
            await _repository.SaveChangesAsync();

            return bm.ToResponse();
        }
    }
}
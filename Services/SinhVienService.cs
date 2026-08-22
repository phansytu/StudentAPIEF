
using StudentAPIw6.Model.request;
using StudentAPIw6.AutoMapper;

using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.Validators.BusinessValidators;
using StudentAPIw6.Common.Wrappers;

namespace StudentAPIw6.Services
{
    public class SinhVienService : ISinhVienService
    {
        private readonly ISinhVienRepository _repository;
        private readonly SinhVienBusinessValidator _businessValidator;

        public SinhVienService(
            ISinhVienRepository repository,
            SinhVienBusinessValidator businessValidator)
        {
            _repository = repository;
            _businessValidator = businessValidator;
        }

        public async Task<PageResponse<SinhVienResponseDTO>> GetAll(SinhVienQueryRequest request)
        {
            var (data, totalCount) = await _repository.GetAllAsync(request);

            return new PageResponse<SinhVienResponseDTO>
            {
                Data = StudentMapper.ToResponseList(data.AsQueryable()),
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
                PageNumber = request.PageNumber
            };
        }

        public async Task<SinhVienResponseDTO> GetSinhVienById(int id)
        {
            var sv = await _businessValidator.CheckId(id); // validator cũng nên gọi qua repository, không đụng DbContext
            return sv.ToResponse();
        }

        public async Task<SinhVienResponseDTO> GetSinhVienByMsv(string masv)
        {
            var sv = await _businessValidator.CheckMsv(masv);
            return sv.ToResponse();
        }

        public async Task<SinhVienResponseDTO> CreateSinhVien(SinhVienRequestDTO.SinhVienCreateDTO createStudentDTO)
        {
            await _businessValidator.CheckEmail(createStudentDTO.Email);

            var student = createStudentDTO.ToEntity();
            await _repository.AddAsync(student);
            await _repository.SaveChangesAsync(); // trước đây thiếu -> giờ có

            return student.ToResponse();
        }

        public async Task<SinhVienResponseDTO> UpdateSinhVien(string maSV, SinhVienRequestDTO.SinhVienUpdateDTO updateStudentDTO)
        {
            var sv = await _businessValidator.CheckIdMsv(maSV);
            sv.updateEntity(updateStudentDTO);

            await _repository.UpdateAsync(sv);
            await _repository.SaveChangesAsync(); // trước đây thiếu -> giờ có

            return sv.ToResponse();
        }

        public async Task<bool> DeleteSinhVien(string maSV)
        {
            var sv = await _businessValidator.CheckMsv(maSV);
            await _repository.DeleteAsync(sv);
            return await _repository.SaveChangesAsync();
        }

        public async Task<PageResponse<SinhVienAdvancedDTO>> GetPagedAdvancedAsync(SinhVienAdvancedRequest request)
        {
            if (request.PageIndex < 1) request.PageIndex = 1;
            if (request.PageSize < 1) request.PageSize = 10;

            if (request.MinDiem.HasValue && request.MaxDiem.HasValue && request.MinDiem > request.MaxDiem)
                throw new ArgumentException("Điểm tối thiểu không được lớn hơn điểm tối đa.");

            return await _repository.GetPagedAdvancedAsync(request);
        }



    }
}
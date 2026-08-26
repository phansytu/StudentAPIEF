
using Microsoft.AspNetCore.Mvc;
using StudentAPIw6.API.DTOs.Request;

using StudentAPIw6.Services.Interfaces;

namespace StudentAPIw6.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SinhVienController : ControllerBase
    {
        private readonly ISinhVienService _service;

        public SinhVienController(ISinhVienService service)
        {
            _service = service;
        }
        //getAll
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] SinhVienQueryRequest request
        )
        {
            var result = await _service.GetAll(request);
            return Ok(result);
        }
        //getbyid
        [HttpGet("{maSV}")]
        public async Task<IActionResult> GetByMsv([FromRoute] string maSV)
        {
            var result = await _service.GetSinhVienByMsv(maSV);

            return Ok(result);
        }
        [HttpGet("sinhvien/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _service.GetSinhVienById(id);

            return Ok(result);
        }

        //create
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] SinhVienRequestDTO.SinhVienCreateDTO dto)
        {
            var result = await _service.CreateSinhVien(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { maSV = result.Id },
                result
            );
        }
        //update
        [HttpPut("{maSV}")]
        public async Task<IActionResult> Update(
            string maSV,
            [FromBody] SinhVienRequestDTO.SinhVienUpdateDTO dto)
        {
            var result = await _service.UpdateSinhVien(
                maSV,
                dto
            );

            return Ok(result);
        }
        [HttpDelete("{maSV}")]
        public async Task<IActionResult> Delete(string maSV)
        {
            await _service.DeleteSinhVien(maSV);

            return NoContent();
        }
        [HttpGet("advanced")]
        public async Task<IActionResult> getAdvancedPage(
             [FromQuery] SinhVienAdvancedRequest request
        )
        {
            var rs = await _service.GetPagedAdvancedAsync(request);
            return Ok(rs);
        }

    }
}
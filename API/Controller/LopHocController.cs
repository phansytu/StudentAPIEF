using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentAPIw6.DTOs;
using StudentAPIw6.Model.request;
using StudentAPIw6.Services;

namespace StudentAPIw6.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LopHocController : ControllerBase
    {
        private readonly ILopHocService _service;
        public LopHocController(ILopHocService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> getAll([FromQuery] PaginationRequest request)
        {
            var all = await _service.GetAllLopHoc(request);
            return Ok(all);
        }
        [HttpGet("{maLop}")]
        public async Task<IActionResult> GetLopHocById([FromRoute] string key)
        {
            var rs = await _service.GetLopHocById(key);
            return Ok(rs);
        }
        [HttpPost]
        public async Task<IActionResult> CreateLopHoc([FromBody] LopHocDTO.LopHocCreateDTO createDTO)
        {
            var rs = await _service.CreateLopHoc(createDTO);
            return CreatedAtAction(
                nameof(GetLopHocById),
                new { maLop = rs.MaLop },
                rs
            );
        }
        [HttpDelete("{maLop}")]
        public async Task<IActionResult> DeleteLopHoc([FromRoute] string key)
        {
            await _service.DeleteLopHoc(key);

            return NoContent();
        }

        [HttpPut("{maLop}")]
        public async Task<IActionResult> Update(
            string maLop,
            [FromBody] LopHocDTO.LopHocUpdateDTO dto)
        {
            var result = await _service.UpdateLopHoc(
                maLop,
                dto
            );

            return Ok(result);
        }
        [HttpGet("thong-ke")]
        public async Task<IActionResult> ThongKe()
        {
            var result = await _service.ThongKeLopHoc();

            return Ok(result);
        }
    }
}
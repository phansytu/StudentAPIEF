using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentAPIw5.dto;
using StudentAPIw5.model.request;
using StudentAPIw5.service;

namespace StudentAPIw5.controller
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
        public async Task<IActionResult> GetById([FromRoute] string maSV)
        {
            var result = await _service.GetSinhVienById(maSV);

            return Ok(result);
        }
        //create
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] SinhVienDTO.SinhVienCreateDTO dto)
        {
            var result = await _service.CreateSinhVien(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { maSV = result.MaSV },
                result
            );
        }
        //update
        [HttpPut("{maSV}")]
        public async Task<IActionResult> Update(
            string maSV,
            [FromBody] SinhVienDTO.SinhVienUpdateDTO dto)
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


    }
}
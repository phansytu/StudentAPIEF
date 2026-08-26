using Microsoft.AspNetCore.Mvc;
using StudentAPIw6.Services.Interfaces;
using StudentAPIw6.API.DTOs.Request;

namespace StudentAPIw6.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoMonController : ControllerBase
    {
        private readonly IBoMonService _service;

        public BoMonController(IBoMonService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetAllBoMon(request);
            return Ok(result);
        }

        [HttpGet("{maBoMon}")]
        public async Task<IActionResult> GetByMa(string maBoMon)
        {
            var result = await _service.GetBoMonByMa(maBoMon);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BoMonRequestDTO.CreateBoMonDTO createDTO)
        {
            var result = await _service.CreateBoMon(createDTO);
            return CreatedAtAction(nameof(GetByMa), new { maBoMon = result.maBM }, result);
        }

        [HttpPut("{maBoMon}")]
        public async Task<IActionResult> Update(string maBoMon, [FromBody] BoMonRequestDTO.UpdateBoMonDTO updateDTO)
        {
            var result = await _service.UpdateBoMon(maBoMon, updateDTO);
            return Ok(result);
        }

        [HttpDelete("{maBoMon}")]
        public async Task<IActionResult> Delete(string maBoMon)
        {
            await _service.DeleteBoMon(maBoMon);
            return NoContent();
        }
    }
}
using Business.Abstract;
using Entities.DTOs.StaffDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateStaffDTO staffDTO)
        {
            var result = await _staffService.CreateStaffAsync(staffDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(string langCode)
        {
            var result = await _staffService.GetAllStaffAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UpdateStaffDTO staffDTO)
        {
            var result = await _staffService.UpdateStaffAsync(staffDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int staffId)
        {
            var result = await _staffService.DeleteStaffAsync(staffId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}

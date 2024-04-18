using Business.Abstract;
using Entities.DTOs.ReferenceDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferencesController : ControllerBase
    {
        private readonly IReferenceService _referenceService;

        public ReferencesController(IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateReferenceDTO referenceDTO)
        {
            var result = await _referenceService.CreateReferenceAsync(referenceDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(string langCode)
        {
            var result = await _referenceService.GetAllReferenceAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UpdateReferenceDTO referenceDTO)
        {
            var result = await _referenceService.UpdateReferenceAsync(referenceDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int referenceId)
        {
            var result = await _referenceService.DeleteReferenceAsync(referenceId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}

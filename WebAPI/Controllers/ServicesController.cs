using Business.Abstract;
using Entities.DTOs.BrandDTOs;
using Entities.DTOs.ServiceDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateServiceDTO serviceDTO)
        {
            var result = await _serviceService.CreateServiceAsync(serviceDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(string langCode)
        {
            var result = await _serviceService.GetAllServiceAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UpdateServiceDTO serviceDTO)
        {
            var result = await _serviceService.UpdateServiceAsync(serviceDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int serviceId)
        {
            var result = await _serviceService.DeleteServiceAsync(serviceId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}

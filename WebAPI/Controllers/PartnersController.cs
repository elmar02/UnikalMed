using Business.Abstract;
using Entities.DTOs.BrandDTOs;
using Entities.DTOs.UploadDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnerService _partnerService;

        public PartnersController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromQuery] string photoUrl)
        {
            var result = await _partnerService.CreatePhotoAsync(photoUrl);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _partnerService.GetAllPhotoAsync();
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int partnerId)
        {
            var result = await _partnerService.DeletePhotoAsync(partnerId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

    }
}

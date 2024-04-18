using Business.Abstract;
using Entities.DTOs.AdvertDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private readonly IAdvertService _advertService;

        public AdvertsController(IAdvertService advertService)
        {
            _advertService = advertService;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateAdvertDTO advertDTO)
        {
            var result = await _advertService.CreatePhotoAsync(advertDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll([FromQuery] string langCode)
        {
            var result = await _advertService.GetAllPhotoAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUnhidden([FromQuery] string langCode)
        {
            var result = await _advertService.GetAllUnhiddenPhotoAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int advertId)
        {
            var result = await _advertService.DeletePhotoAsync(advertId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}

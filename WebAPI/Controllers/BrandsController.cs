using Business.Abstract;
using Entities.DTOs.BrandDTOs;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateBrandDTO brandDTO)
        {
            var result = await _brandService.CreateBrandAsync(brandDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery]int brandId)
        {
            var result = await _brandService.GetBrandAsync(brandId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _brandService.GetAllBrandAsync();
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllDeleted()
        {
            var result = await _brandService.GetAllDeletedBrandAsync();
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public IActionResult SoftDelete([FromQuery] int brandId)
        {
            var result = _brandService.SoftDeleteBrand(brandId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("[action]")]
        public IActionResult Restore([FromQuery] int brandId)
        {
            var result = _brandService.RestoreBrand(brandId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UpdateBrandDTO brandDTO)
        {
            var result = await _brandService.UpdateProductAsync(brandDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        //[HttpDelete("[action]")]
        //public IActionResult HardDelete([FromQuery]int brandId)
        //{
        //    var result = _brandService.HardDeleteBrand(brandId);
        //    if (!result.Success) return BadRequest(result);
        //    return Ok(result);
        //}
    }
}

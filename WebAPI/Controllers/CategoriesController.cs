using Business.Abstract;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO categoryDTO)
        {
            var result = await _categoryService.CreateCategoryAsync(categoryDTO);
            if(!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int categoryId, string langCode)
        {
            var result = await _categoryService.GetCategoryAsync(categoryId,langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetWithLang(int categoryId)
        {
            var result = await _categoryService.GetCategoryWithLang(categoryId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(string langCode)
        {
            var result = await _categoryService.GetAllCategoryAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllDeleted(string langCode)
        {
            var result = await _categoryService.GetAllDeletedCategoryAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCatalog(string langCode)
        {
            var result = await _categoryService.GetCatalogAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO categoryDTO)
        {
            var result = await _categoryService.UpdateCategoryAsync(categoryDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public IActionResult SoftDelete(int categoryId)
        {
            var result = _categoryService.SoftDeleteCategory(categoryId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("[action]")]
        public IActionResult Restore(int categoryId)
        {
            var result = _categoryService.RestoreCategory(categoryId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
        //[HttpDelete("[action]")]
        //public IActionResult HardDelete(int categoryId)
        //{
        //    var result = _categoryService.HardDeleteCategory(categoryId);
        //    if (!result.Success) return BadRequest(result);
        //    return Ok(result);
        //}
    }
}

using Business.Abstract;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.SubCategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateSubCategoryDTO categoryDTO)
        {
            var result = await _subCategoryService.CreateSubCategoryAsync(categoryDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int subCategoryId, string langCode)
        {
            var result = await _subCategoryService.GetSubCategoryAsync(subCategoryId, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetWithLang(int subCategoryId, string langCode)
        {
            var result = await _subCategoryService.GetSubCategoryWithLang(subCategoryId, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(string langCode)
        {
            var result = await _subCategoryService.GetAllSubCategoryAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllDeleted(string langCode)
        {
            var result = await _subCategoryService.GetAllDeletedSubCategoryAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateSubCategoryDTO categoryDTO)
        {
            var result = await _subCategoryService.UpdateSubCategoryAsync(categoryDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public IActionResult SoftDelete(int subCategoryId)
        {
            var result = _subCategoryService.SoftDeleteSubCategory(subCategoryId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public IActionResult Restore(int subCategoryId)
        {
            var result = _subCategoryService.RestoreSubCategory(subCategoryId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        //[HttpDelete("[action]")]
        //public IActionResult HardDelete(int subCategoryId)
        //{
        //    var result = _subCategoryService.HardDeleteSubCategory(subCategoryId);
        //    if (!result.Success) return BadRequest(result);
        //    return Ok(result);
        //}
    }
}

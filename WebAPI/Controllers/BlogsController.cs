using Business.Abstract;
using Entities.DTOs.BlogDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateBlogDTO blogDTO)
        {
            var result = await _blogService.CreateBlogAsync(blogDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int blogId,string langCode)
        {
            var result = await _blogService.GetBlogDetailAsync(blogId, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(int page, int limit, string langCode)
        {
            var result = await _blogService.GetAllBlogAsync(page, limit, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllLatest(string langCode)
        {
            var result = await _blogService.GetLatestBlogAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UpdateBlogDTO blogDTO)
        {
            var result = await _blogService.UpdateBlogAsync(blogDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int blogId)
        {
            var result = await _blogService.DeleteBlogAsync(blogId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}

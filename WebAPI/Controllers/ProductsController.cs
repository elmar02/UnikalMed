using Business.Abstract;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO productDTO)
        {
            var result = await _productService.CreateProductAsync(productDTO);
            if (!result.Success) return BadRequest();
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] int productId, [FromQuery] string langCode)
        {
            var result = await _productService.GetProductAsync(productId, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll([FromQuery] string langCode)
        {
            var result = await _productService.GetAllProductAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetWithLang([FromQuery] int productId,[FromQuery] string langCode)
        {
            var result = await _productService.GetProductWithLang(productId, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllDeleted([FromQuery] string langCode)
        {
            var result = await _productService.GetAllDeletedProductAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNewComes([FromQuery] string langCode)
        {
            var result = await _productService.GetNewComesAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTrendyProducts([FromQuery] string langCode)
        {
            var result = await _productService.GetTrendyProductsAsync(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductDetail([FromQuery] int productId, [FromQuery] string langCode)
        {
            var result = await _productService.GetProductDetailAsync(productId,langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSimilarProducts([FromQuery] int productId, [FromQuery] string langCode)
        {
            var result = await _productService.GetSimilarProductsAsync(productId, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBrandProducts([FromQuery] int productId, [FromQuery] string langCode)
        {
            var result = await _productService.GetBrandProductsAsync(productId, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFilteredProducts([FromQuery] int categoryId, [FromQuery] int subCategoryId, [FromQuery] int page, [FromQuery] int limit, [FromQuery] string langCode)
        {
            var result = await _productService.GetFilteredProductsAsync(categoryId, subCategoryId, page, limit, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoryFilteredProducts([FromQuery]int categoryId, [FromQuery]List<string> values, [FromQuery] int page, [FromQuery] int limit, [FromQuery] string langCode)
        {
            var result = await _productService.GetFilteredCategoryProductsAsync(categoryId, values, page, limit, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubCategoryFilteredProducts([FromQuery] int categoryId,[FromQuery] int subCategoryId, [FromQuery] List<string> values, [FromQuery] int page, [FromQuery] int limit, [FromQuery] string langCode)
        {
            var result = await _productService.GetFilteredSubCategoryProductsAsync(categoryId,subCategoryId, values, page, limit, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery] string productName, [FromQuery] string langCode)
        {
            var result = await _productService.SearchProducts(productName, langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductSliders([FromQuery] string langCode)
        {
            var result = await _productService.GetProductSliders(langCode);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public IActionResult SoftDelete([FromQuery] int productId)
        {
            var result = _productService.SoftDeleteProduct(productId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("[action]")]
        public IActionResult Restore([FromQuery] int productId)
        {
            var result = _productService.RestoreProduct(productId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateProductDTO productDTO)
        {
            var result = await _productService.UpdateProductAsync(productDTO);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}

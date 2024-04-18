using Core.Utilities.Results.Abstract;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IResult> CreateProductAsync(CreateProductDTO productDTO);
        Task<IDataResult<GetProductDTO>> GetProductAsync(int productId, string langCode);
        Task<IDataResult<List<GetProductListDTO>>> GetAllProductAsync(string langCode);
        Task<IDataResult<List<GetProductListDTO>>> GetAllDeletedProductAsync(string langCode);
        Task<IDataResult<GetProductLangDTO>> GetProductWithLang(int productId, string langCode);
        IResult SoftDeleteProduct(int productId);
        IResult RestoreProduct(int productId);
        Task<IResult> UpdateProductAsync(UpdateProductDTO productDTO);
        Task<IDataResult<List<NewComesDTO>>> GetNewComesAsync(string langCode);
        Task<IDataResult<List<TrendyProductsDTO>>> GetTrendyProductsAsync(string langCode);
        Task<IDataResult<ProductDetailDTO>> GetProductDetailAsync(int productId, string langCode);
        Task<IDataResult<List<SimilarProductsDTO>>> GetSimilarProductsAsync(int productId, string langCode);
        Task<IDataResult<List<BrandsProductsDTO>>> GetBrandProductsAsync(int productId, string langCode);
        Task<IDataResult<FilterProductDTO>> GetFilteredProductsAsync(int categoryId, int subCategoryId, int page, int limit, string langCode);
        Task<IDataResult<FilterCategoryProductDTO>> GetFilteredCategoryProductsAsync(int categoryId, List<string> values, int page, int limit, string langCode);
        Task<IDataResult<FilterCategoryProductDTO>> GetFilteredSubCategoryProductsAsync(int categoryId, int subCategoryId, List<string> values, int page, int limit, string langCode);
        Task<IDataResult<List<SearchProductDTO>>> SearchProducts(string productName, string langCode);
        Task<IDataResult<List<ProductSliderDTO>>> GetProductSliders(string langCode);
    }
}

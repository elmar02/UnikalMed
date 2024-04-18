using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.ProductEntities;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDAL : IRepositoryBase<Product>
    {
        Task<IResult> CreateProductAsync(CreateProductDTO productDTO);
        Task<GetProductDTO> GetProductAsync(int productId, string langCode);
        Task<List<GetProductListDTO>> GetAllProductAsync(string langCode);
        Task<List<GetProductListDTO>> GetAllDeletedProductAsync(string langCode);
        Task<GetProductLangDTO> GetProductWithLangAsync(int productId, string langCode);
        IResult SoftDeleteProduct(int productId);
        IResult RestoreProduct(int productId);
        Task<IResult> UpdateProductAsync(UpdateProductDTO productDTO);
        Task<List<NewComesDTO>> GetNewComesAsync(string langCode);
        Task<List<TrendyProductsDTO>> GetTrendyProductsAsync(string langCode);
        Task<ProductDetailDTO> GetProductDetailAsync(int productId, string langCode);
        Task<List<SimilarProductsDTO>> GetSimilarProductsAsync(int productId, string langCode);
        Task<List<BrandsProductsDTO>> GetBrandProductsAsync(int productId, string langCode);
        Task<FilterProductDTO> GetFilteredProductsAsync(int categoryId, int subCategoryId, int page, int limit, string langCode);
        Task<FilterCategoryProductDTO> GetFilteredCategoryProductsAsync(int categoryId , List<string> values, int page, int limit, string langCode);
        Task<FilterCategoryProductDTO> GetFilteredSubCategoryProductsAsync(int categoryId, int subCategoryId, List<string> values, int page, int limit, string langCode);
        Task<List<SearchProductDTO>> SearchProducts(string productName, string langCode);
        Task<List<ProductSliderDTO>> GetProductSliders(string langCode);
    }
}

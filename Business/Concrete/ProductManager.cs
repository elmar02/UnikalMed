using Business.Abstract;
using Business.Messages;
using Business.Validation.CategoryValidation;
using Business.Validation.ProductValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.SQLServer;
using Entities.Concrete.CategoryEntities;
using Entities.Concrete.ProductEntities;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;
        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public async Task<IResult> CreateProductAsync(CreateProductDTO productDTO)
        {
            try
            {
                var validator = new CreateProductValidation();
                var validationResult = await validator.ValidateAsync(productDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.ToString(), statusCode: HttpStatusCode.BadRequest);
                var productResult = await _productDAL.CreateProductAsync(productDTO);
                return productResult;
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetProductListDTO>>> GetAllDeletedProductAsync(string langCode)
        {
            try
            {
                var result = await _productDAL.GetAllDeletedProductAsync(langCode);
                return new SuccessDataResult<List<GetProductListDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<GetProductListDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetProductListDTO>>> GetAllProductAsync(string langCode)
        {
            try
            {
                var result = await _productDAL.GetAllProductAsync(langCode);
                return new SuccessDataResult<List<GetProductListDTO>> (data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<GetProductListDTO>> (message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<BrandsProductsDTO>>> GetBrandProductsAsync(int productId, string langCode)
        {
            try
            {
                var result = await _productDAL.GetBrandProductsAsync(productId, langCode);
                if (result == null) return new ErrorDataResult<List<BrandsProductsDTO>>(message: MessageStatus.NotFoundMessage("Məhsul"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<BrandsProductsDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<BrandsProductsDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<FilterCategoryProductDTO>> GetFilteredCategoryProductsAsync(int categoryId, List<string> values, int page, int limit, string langCode)
        {
            try
            {
                var result = await _productDAL.GetFilteredCategoryProductsAsync(categoryId, values, page, limit, langCode);
                if (result == null) return new ErrorDataResult<FilterCategoryProductDTO>(message: MessageStatus.NotFoundMessage("Məhsul"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<FilterCategoryProductDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<FilterCategoryProductDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<FilterProductDTO>> GetFilteredProductsAsync(int categoryId, int subCategoryId, int page, int limit, string langCode)
        {
            try
            {
                var result = await _productDAL.GetFilteredProductsAsync(categoryId, subCategoryId, page, limit, langCode);
                if (result == null) return new ErrorDataResult<FilterProductDTO>(message: MessageStatus.NotFoundMessage("Məhsul"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<FilterProductDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<FilterProductDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<FilterCategoryProductDTO>> GetFilteredSubCategoryProductsAsync(int categoryId, int subCategoryId, List<string> values, int page, int limit, string langCode)
        {
            try
            {
                var result = await _productDAL.GetFilteredSubCategoryProductsAsync(categoryId,subCategoryId, values, page, limit, langCode);
                if (result == null) return new ErrorDataResult<FilterCategoryProductDTO>(message: MessageStatus.NotFoundMessage("Məhsul"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<FilterCategoryProductDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<FilterCategoryProductDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<NewComesDTO>>> GetNewComesAsync(string langCode)
        {
            try
            {
                var result = await _productDAL.GetNewComesAsync(langCode);
                return new SuccessDataResult<List<NewComesDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<NewComesDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<GetProductDTO>> GetProductAsync(int productId, string langCode)
        {
            try
            {
                var result = await _productDAL.GetProductAsync(productId, langCode);
                if (result == null) return new ErrorDataResult<GetProductDTO>(message: MessageStatus.NotFoundMessage("Məhsul"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<GetProductDTO>(data: result, statusCode:HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<GetProductDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<ProductDetailDTO>> GetProductDetailAsync(int productId, string langCode)
        {
            try
            {
                var result = await _productDAL.GetProductDetailAsync(productId, langCode);
                if (result == null) return new ErrorDataResult<ProductDetailDTO>(message: MessageStatus.NotFoundMessage("Məhsul"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<ProductDetailDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<ProductDetailDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<ProductSliderDTO>>> GetProductSliders(string langCode)
        {
            try
            {
                var result = await _productDAL.GetProductSliders(langCode);
                return new SuccessDataResult<List<ProductSliderDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<ProductSliderDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<GetProductLangDTO>> GetProductWithLang(int productId, string langCode)
        {
            try
            {
                var result = await _productDAL.GetProductWithLangAsync(productId, langCode);
                if (result == null) return new ErrorDataResult<GetProductLangDTO>(message: MessageStatus.NotFoundMessage("məhsul"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<GetProductLangDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<GetProductLangDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<SimilarProductsDTO>>> GetSimilarProductsAsync(int productId, string langCode)
        {
            try
            {
                var result = await _productDAL.GetSimilarProductsAsync(productId, langCode);
                if (result == null) return new ErrorDataResult<List<SimilarProductsDTO>>(message: MessageStatus.NotFoundMessage("Məhsul"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<SimilarProductsDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<SimilarProductsDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<TrendyProductsDTO>>> GetTrendyProductsAsync(string langCode)
        {
            try
            {
                var result = await _productDAL.GetTrendyProductsAsync(langCode);
                return new SuccessDataResult<List<TrendyProductsDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<TrendyProductsDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult RestoreProduct(int productId)
        {
            try
            {
                var result = _productDAL.RestoreProduct(productId);
                return result;
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<SearchProductDTO>>> SearchProducts(string productName, string langCode)
        {
            try
            {
                var result = await _productDAL.SearchProducts(productName, langCode);
                return new SuccessDataResult<List<SearchProductDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<SearchProductDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult SoftDeleteProduct(int productId)
        {
            try
            {
                var result = _productDAL.SoftDeleteProduct(productId);
                return result;
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateProductAsync(UpdateProductDTO productDTO)
        {
            try
            {
                var validator = new UpdateProductValidation();
                var validationResult = await validator.ValidateAsync(productDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.ToString(), statusCode: HttpStatusCode.BadRequest);

                var productResult = await _productDAL.UpdateProductAsync(productDTO);
                return productResult;
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}

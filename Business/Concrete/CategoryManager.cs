using Business.Abstract;
using Business.Messages;
using Business.Validation.CategoryValidation;
using Business.Validation.SubCategoryValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.Concrete.CategoryEntities;
using Entities.DTOs.CategoryDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;
        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public async Task<IResult> CreateCategoryAsync( CreateCategoryDTO categoryDTO)
        {
			try
			{
                var validator = new CreateCategoryValidation();
                var validationResult = await validator.ValidateAsync(categoryDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.ToString(), statusCode: HttpStatusCode.BadRequest);
                var result = await _categoryDAL.CreateCategoryAsync(categoryDTO);
                if (!result.Success) return result;
                return new SuccessResult(statusCode: HttpStatusCode.OK);
			}
			catch (Exception)
			{
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
			}
        }

        public async Task<IDataResult<List<GetCategoryListDTO>>> GetAllCategoryAsync(string langCode)
        {
            try
            {
                var result = await _categoryDAL.GetAllCategoriesAsync(langCode);
                if (result == null) return new ErrorDataResult<List<GetCategoryListDTO>>(message: MessageStatus.NotFoundMessage("kateqoriya"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<GetCategoryListDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<GetCategoryListDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetCategoryListDTO>>> GetAllDeletedCategoryAsync(string langCode)
        {
            try
            {
                var result = await _categoryDAL.GetAllDeletedCategoriesAsync(langCode);
                if (result == null) return new ErrorDataResult<List<GetCategoryListDTO>>(message: MessageStatus.NotFoundMessage("kateqoriya"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<GetCategoryListDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<GetCategoryListDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<CatalogDTO>>> GetCatalogAsync(string langCode)
        {
            try
            {
                var data = await _categoryDAL.GetCategoryCatalogAsync(langCode);
                return new SuccessDataResult<List<CatalogDTO>>(data: data, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<CatalogDTO>>(HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<GetCategoryDTO>> GetCategoryAsync(int categoryId, string langCode)
        {
            try
            {
                var result = await _categoryDAL.GetCategoryAsync(categoryId, langCode);
                if(result == null) return new ErrorDataResult<GetCategoryDTO>(message: MessageStatus.NotFoundMessage("kateqoriya"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<GetCategoryDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<GetCategoryDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<GetCategoryLangDTO>> GetCategoryWithLang(int categoryId)
        {
            try
            {
                var result = await _categoryDAL.GetCategoryWithLangAsync(categoryId);
                if (result == null) return new ErrorDataResult<GetCategoryLangDTO>(message: MessageStatus.NotFoundMessage("kateqoriya"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<GetCategoryLangDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<GetCategoryLangDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult HardDeleteCategory(int categoryId)
        {
            try
            {
                var result = _categoryDAL.HardDeleteCategory(categoryId);
                if(!result) return new SuccessResult(HttpStatusCode.BadRequest);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult RestoreCategory(int categoryId)
        {
            try
            {
                var result = _categoryDAL.RestoreCategory(categoryId);
                return result;
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult SoftDeleteCategory(int categoryId)
        {
            try
            {
                var result = _categoryDAL.SoftDeleteCategory(categoryId);
                return result;
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateCategoryAsync(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                var validator = new UpdateCategoryValidation();
                var validationResult = await validator.ValidateAsync(categoryDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.ToString(), statusCode: HttpStatusCode.BadRequest);
                var result = _categoryDAL.UpdateCategory(categoryDTO);
                if (!result.Success) return result;
                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }

        }
    }
}

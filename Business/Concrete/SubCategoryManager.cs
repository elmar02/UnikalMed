using Business.Abstract;
using Business.Messages;
using Business.Validation.CategoryValidation;
using Business.Validation.SubCategoryValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.SQLServer;
using Entities.Concrete.CategoryEntities;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.SubCategoryDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SubCategoryManager : ISubCategoryService
    {
        private readonly ISubCategoryDAL _subCategoryDAL;

        public SubCategoryManager(ISubCategoryDAL subCategoryDAL)
        {
            _subCategoryDAL = subCategoryDAL;
        }

        public async Task<IResult> CreateSubCategoryAsync(CreateSubCategoryDTO categoryDTO)
        {
			try
			{
                var validator = new CreateSubCategoryValidation();
                var validationResult = await validator.ValidateAsync(categoryDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.ToString(), statusCode: HttpStatusCode.BadRequest);
                var result = await _subCategoryDAL.CreateSubCategoryAsync(categoryDTO);
                if (!result.Success) return result;
                return new SuccessResult(message: "alt kateqoriya yaradildi", statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
			{
                return new ErrorResult(message: "Gozlenilmez Error Bas verdi", statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetSubCategoryDTO>>> GetAllDeletedSubCategoryAsync(string langCode)
        {
            try
            {
                var result = await _subCategoryDAL.GetAllDeletedSubCategoriesAsync(langCode);
                if (result == null) return new ErrorDataResult<List<GetSubCategoryDTO>>(message: MessageStatus.NotFoundMessage("alt kateqoriya"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<GetSubCategoryDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<GetSubCategoryDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetSubCategoryDTO>>> GetAllSubCategoryAsync(string langCode)
        {
            try
            {
                var result = await _subCategoryDAL.GetAllSubCategoriesAsync(langCode);
                if (result == null) return new ErrorDataResult<List<GetSubCategoryDTO>>(message: MessageStatus.NotFoundMessage("alt kateqoriya"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<GetSubCategoryDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<GetSubCategoryDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<GetSubCategoryDTO>> GetSubCategoryAsync(int subCategoryId, string langCode)
        {
            try
            {
                var result = await _subCategoryDAL.GetSubCategoryAsync(subCategoryId, langCode);
                if (result == null) return new ErrorDataResult<GetSubCategoryDTO>(message: MessageStatus.NotFoundMessage("alt kateqoriya"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<GetSubCategoryDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<GetSubCategoryDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<GetSubCategoryLangDTO>> GetSubCategoryWithLang(int subCategoryId, string langCode)
        {
            try
            {
                var result = await _subCategoryDAL.GetSubCategoryWithLangAsync(subCategoryId, langCode);
                if (result == null) return new ErrorDataResult<GetSubCategoryLangDTO>(message: MessageStatus.NotFoundMessage("alt kateqoriya"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<GetSubCategoryLangDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<GetSubCategoryLangDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult HardDeleteSubCategory(int subCategoryId)
        {
            try
            {
                var result = _subCategoryDAL.HardDeleteSubCategory(subCategoryId);
                if (!result) return new SuccessResult(HttpStatusCode.BadRequest);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult RestoreSubCategory(int subCategoryId)
        {
            try
            {
                var result = _subCategoryDAL.RestoreSubCategory(subCategoryId);
                return result;
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult SoftDeleteSubCategory(int subCategoryId)
        {
            try
            {
                var result = _subCategoryDAL.SoftDeleteSubCategory(subCategoryId);
                return result;
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateSubCategoryAsync(UpdateSubCategoryDTO categoryDTO)
        {
            try
            {
                var validator = new UpdateSubCategoryValidation();
                var validationResult = await validator.ValidateAsync(categoryDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.ToString(), statusCode: HttpStatusCode.BadRequest);
                var result = _subCategoryDAL.UpdateSubCategory(categoryDTO);
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

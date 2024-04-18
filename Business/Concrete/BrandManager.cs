using Business.Abstract;
using Business.Messages;
using Business.Validation.BrandValidation;
using Business.Validation.CategoryValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.SQLServer;
using Entities.Concrete;
using Entities.Concrete.CategoryEntities;
using Entities.DTOs.BrandDTOs;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDAL _brandDAL;

        public BrandManager(IBrandDAL brandDAL)
        {
            _brandDAL = brandDAL;
        }

        public async Task<IResult> CreateBrandAsync(CreateBrandDTO createBrandDTO)
        {
            try
            {
                try
                {
                    var validator = new CreateBrandValidation();
                    var validationResult = await validator.ValidateAsync(createBrandDTO);
                    if (!validationResult.IsValid)
                        return new ErrorResult(message: validationResult.ToString(), statusCode: HttpStatusCode.BadRequest);
                    var result = await _brandDAL.CreateBrandAsync(createBrandDTO);
                    if (!result) return new ErrorResult(message: MessageStatus.ExistMessage("Brend"), statusCode: HttpStatusCode.BadRequest);
                    return new SuccessResult(statusCode: HttpStatusCode.OK);
                }
                catch (Exception)
                {
                    return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IResult HardDeleteBrand(int brandId)
        {
            try
            {
                var result = _brandDAL.HardDeleteBrand(brandId);
                if (!result) return new ErrorResult(message: MessageStatus.NotFoundMessage("brend"), HttpStatusCode.BadRequest);
                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetBrandListDTO>>> GetAllBrandAsync()
        {
            try
            {
                var result = await _brandDAL.GetAllBrandAsync();
                if (result == null) return new ErrorDataResult<List<GetBrandListDTO>>(message: MessageStatus.NotFoundMessage("brend"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<GetBrandListDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<GetBrandListDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetBrandListDTO>>> GetAllDeletedBrandAsync()
        {
            try
            {
                var result = await _brandDAL.GetAllDeletedBrandAsync();
                if (result == null) return new ErrorDataResult<List<GetBrandListDTO>>(message: MessageStatus.NotFoundMessage("brend"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<GetBrandListDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<GetBrandListDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<GetBrandDTO>> GetBrandAsync(int brandId)
        {
            try
            {
                var result = await _brandDAL.GetBrandAsync(brandId);
                if (result == null) return new ErrorDataResult<GetBrandDTO>(message: MessageStatus.NotFoundMessage("brend"), HttpStatusCode.BadRequest);
                return new SuccessDataResult<GetBrandDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<GetBrandDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult RestoreBrand(int brandId)
        {
            try
            {
                var result = _brandDAL.RestoreBrand(brandId);
                if (!result) return new ErrorResult(message: MessageStatus.NotFoundMessage("brend"), HttpStatusCode.BadRequest);
                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult SoftDeleteBrand(int brandId)
        {
            try
            {
                var result = _brandDAL.SoftDeleteBrand(brandId);
                if (!result) return new ErrorResult(message: MessageStatus.NotFoundMessage("brend"), HttpStatusCode.BadRequest);
                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateProductAsync(UpdateBrandDTO brandDTO)
        {
            try
            {

                var validator = new UpdateBrandValidation();
                var validationResult = await validator.ValidateAsync(brandDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.ToString(), statusCode: HttpStatusCode.BadRequest);
                var result = await _brandDAL.UpdateBrandAsync(brandDTO);
                if (!result) return new ErrorResult(message: MessageStatus.NotFoundMessage("Brend"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}

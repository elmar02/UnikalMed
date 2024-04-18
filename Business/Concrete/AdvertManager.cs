using Business.Abstract;
using Business.Messages;
using Business.Utilities.Uploader;
using Business.Validation.AdvertValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.SQLServer;
using Entities.DTOs.AdvertDTOs;
using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdvertManager : IAdvertService
    {
        private readonly IAdvertDAL _advertDAL;
        private readonly IFileService _fileService;

        public AdvertManager(IAdvertDAL advertDAL, IFileService storageService)
        {
            _advertDAL = advertDAL;
            _fileService = storageService;
        }

        public async Task<IResult> CreatePhotoAsync(CreateAdvertDTO advertDTO)
        {
            try
            {
                var validation = new CreateAdvertValidation();
                var result = await validation.ValidateAsync(advertDTO);
                if(!result.IsValid) 
                    return new ErrorResult(message: result.Errors.ToString(), statusCode: HttpStatusCode.BadRequest);
                await _advertDAL.CreatePhotoAsync(advertDTO);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeletePhotoAsync(int advertId)
        {
            try
            {
                var result = await _advertDAL.DeletePhotoAsync(advertId);
                if (result == null) return new ErrorResult(message: MessageStatus.NotFoundMessage("Şəkil"), statusCode: HttpStatusCode.BadRequest);
                foreach (var photo in result)
                {
                    _fileService.Delete(photo);
                }
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<AdvertDTO>>> GetAllPhotoAsync(string langCode)
        {
            try
            {
                var photos = await _advertDAL.GetAllPhotoAsync(langCode);
                return new SuccessDataResult<List<AdvertDTO>>(data: photos, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<AdvertDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<UnhiddenAdvertDTO>>> GetAllUnhiddenPhotoAsync(string langCode)
        {
            try
            {
                var photos = await _advertDAL.GetAllUnhiddenPhotoAsync(langCode);
                return new SuccessDataResult<List<UnhiddenAdvertDTO>>(data: photos, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<UnhiddenAdvertDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}

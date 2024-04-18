using Business.Abstract;
using Business.Messages;
using Business.Utilities.Uploader;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.SQLServer;
using Entities.DTOs.PartnerDTOs;
using Entities.DTOs.UploadDTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PartnerManager : IPartnerService
    {
        private readonly IPartnerDAL _partnerDAL;
        private readonly IFileService _fileService;

        public PartnerManager(IPartnerDAL partnerDAL, IFileService storageService)
        {
            _partnerDAL = partnerDAL;
            _fileService = storageService;
        }

        public async Task<IResult> CreatePhotoAsync(string photoUrl)
        {
            if (photoUrl.IsNullOrEmpty())
                return new ErrorResult(message: MessageStatus.NotNullMessage("Şəkil"), statusCode: HttpStatusCode.BadRequest);
            try
            {
                await _partnerDAL.CreatePhotoAsync(photoUrl);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
			{
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
			}
        }

        public async Task<IResult> DeletePhotoAsync(int partnerId)
        {
            try
            {
                var result = await _partnerDAL.DeletePhotoAsync(partnerId);
                if (result == null) return new ErrorResult(message: MessageStatus.NotFoundMessage("Şəkil"), statusCode: HttpStatusCode.BadRequest);
                _fileService.Delete(result);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<PartnerDTO>>> GetAllPhotoAsync()
        {
            try
            {
                var photos = await _partnerDAL.GetAllPhotoAsync();
                return new SuccessDataResult<List<PartnerDTO>>(data: photos, statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return new ErrorDataResult<List<PartnerDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}

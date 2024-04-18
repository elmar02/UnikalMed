using Business.Abstract;
using Business.Messages;
using Business.Utilities.Uploader;
using Business.Validation.ReferenceValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.DTOs.ReferenceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReferenceManager : IReferenceService
    {
        private readonly IReferenceDAL _referenceDAL;
        private readonly IFileService _fileService;
        public ReferenceManager(IReferenceDAL referenceDAL, IFileService storageService)
        {
            _referenceDAL = referenceDAL;
            _fileService = storageService;
        }

        public async Task<IResult> CreateReferenceAsync(CreateReferenceDTO referenceDTO)
        {
            try
            {
                var validation = new CreateReferenceValidation();
                var validationResult = await validation.ValidateAsync(referenceDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.Errors.ToString(), statusCode: HttpStatusCode.BadRequest);

                var result = await _referenceDAL.CreateReferenceAsync(referenceDTO);
                if (!result)
                    return new ErrorResult(message: MessageStatus.ExistMessage("Referans"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeleteReferenceAsync(int ReferenceId)
        {
            try
            {
                var result = _referenceDAL.DeleteReference(ReferenceId);
                if (result == null)
                    return new ErrorResult(message: MessageStatus.NotFoundMessage("Referans"), statusCode: HttpStatusCode.BadRequest);
                _fileService.Delete(result);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetReferenceListDTO>>> GetAllReferenceAsync(string langCode)
        {
            try
            {
                var result = await _referenceDAL.GetAllReferenceAsync(langCode);
                if (result == null)
                    return new ErrorDataResult<List<GetReferenceListDTO>>(message: MessageStatus.NotFoundMessage("Referans"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<GetReferenceListDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new ErrorDataResult<List<GetReferenceListDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateReferenceAsync(UpdateReferenceDTO referenceDTO)
        {
            try
            {
                var result = await _referenceDAL.UpdateReferenceAsync(referenceDTO);
                if (result == null)
                    return new ErrorResult(message: MessageStatus.NotFoundMessage("Referans"), statusCode: HttpStatusCode.BadRequest);
                _fileService.Delete(result);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}

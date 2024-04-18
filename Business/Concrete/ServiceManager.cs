using Business.Abstract;
using Business.Messages;
using Business.Utilities.Uploader;
using Business.Validation.ServiceValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.DTOs.ServiceDTOs;
using System.Net;

namespace Business.Concrete
{
    public class ServiceManager : IServiceService
    {
        private readonly IServiceDAL _serviceDAL;
        private readonly IFileService _fileService;
        public ServiceManager(IServiceDAL serviceDAL, IFileService storageService)
        {
            _serviceDAL = serviceDAL;
            _fileService = storageService;
        }

        public async Task<IResult> CreateServiceAsync(CreateServiceDTO serviceDTO)
        {
            try
            {
                var validation = new CreateServiceValidation();
                var validationResult = await validation.ValidateAsync(serviceDTO);
                if(!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.Errors.ToString(), statusCode: HttpStatusCode.BadRequest);

                var result = await _serviceDAL.CreateServiceAsync(serviceDTO);
                if(!result)
                    return new ErrorResult(message: MessageStatus.ExistMessage("Servis"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeleteServiceAsync(int serviceId)
        {
            try
            {
                var result = _serviceDAL.DeleteService(serviceId);
                if (result == null)
                    return new ErrorResult(message: MessageStatus.NotFoundMessage("Servis"), statusCode: HttpStatusCode.BadRequest);
                _fileService.Delete(result);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetServiceListDTO>>> GetAllServiceAsync(string langCode)
        {
            try
            {
                var result = await _serviceDAL.GetAllServiceAsync(langCode);
                if (result == null)
                    return new ErrorDataResult<List<GetServiceListDTO>>( message: MessageStatus.NotFoundMessage("Servis"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<GetServiceListDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new ErrorDataResult<List<GetServiceListDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateServiceAsync(UpdateServiceDTO serviceDTO)
        {
            try
            {
                var result = await _serviceDAL.UpdateServiceAsync(serviceDTO);
                if (result == null)
                    return new ErrorResult(message: MessageStatus.NotFoundMessage("Servis"), statusCode: HttpStatusCode.BadRequest);
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

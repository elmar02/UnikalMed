using Business.Abstract;
using Business.Messages;
using Business.Utilities.Uploader;
using Business.Validation.StaffValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.DTOs.StaffDTOs;
using System.Net;

namespace Business.Concrete
{
    public class StaffManager : IStaffService
    {
        private readonly IStaffDAL _staffDAL;
        private readonly IFileService _fileService;
        public StaffManager(IStaffDAL staffDAL, IFileService storageService)
        {
            _staffDAL = staffDAL;
            _fileService = storageService;
        }

        public async Task<IResult> CreateStaffAsync(CreateStaffDTO staffDTO)
        {
            try
            {
                var validation = new CreateStaffValidation();
                var validationResult = await validation.ValidateAsync(staffDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.Errors.ToString(), statusCode: HttpStatusCode.BadRequest);

                var result = await _staffDAL.CreateStaffAsync(staffDTO);
                if (!result)
                    return new ErrorResult(message: MessageStatus.ExistMessage("Rəhbər"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeleteStaffAsync(int staffId)
        {
            try
            {
                var result = _staffDAL.DeleteStaff(staffId);
                if (result == null)
                    return new ErrorResult(message: MessageStatus.NotFoundMessage("Rəhbər"), statusCode: HttpStatusCode.BadRequest);
                _fileService.Delete(result);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetStaffListDTO>>> GetAllStaffAsync(string langCode)
        {
            try
            {
                var result = await _staffDAL.GetAllStaffAsync(langCode);
                if (result == null)
                    return new ErrorDataResult<List<GetStaffListDTO>>(message: MessageStatus.NotFoundMessage("Rəhbər"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<GetStaffListDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new ErrorDataResult<List<GetStaffListDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateStaffAsync(UpdateStaffDTO staffDTO)
        {
            try{
                var result = await _staffDAL.UpdateStaffAsync(staffDTO);
                if (result == null)
                    return new ErrorResult(message: MessageStatus.NotFoundMessage("Rəhbər"), statusCode: HttpStatusCode.BadRequest);
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

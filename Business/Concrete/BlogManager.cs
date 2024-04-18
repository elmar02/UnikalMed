using Business.Abstract;
using Business.Messages;
using Business.Utilities.Uploader;
using Business.Validation.BlogValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.DTOs.BlogDTOs;
using System.Net;

namespace Business.Concrete
{
    public class BlogManager : IBlogService
    {
        private readonly IBlogDAL _blogDAL;
        private readonly IFileService _fileService;

        public BlogManager(IBlogDAL blogDAL, IFileService storageService)
        {
            _blogDAL = blogDAL;
            _fileService = storageService;
        }

        public async Task<IResult> CreateBlogAsync(CreateBlogDTO blogDTO)
        {
            try
            {
                var validation = new CreateBlogValidation();
                var validationResult = await validation.ValidateAsync(blogDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.Errors.ToString(), statusCode: HttpStatusCode.BadRequest);

                var result = await _blogDAL.CreateBlogAsync(blogDTO);
                if (!result)
                    return new ErrorResult(message: MessageStatus.ExistMessage("Bloq"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeleteBlogAsync(int blogId)
        {
            try{
                var result = _blogDAL.DeleteBlog(blogId);
                if (result == null)
                    return new ErrorResult(message: MessageStatus.NotFoundMessage("Bloq"), statusCode: HttpStatusCode.BadRequest);
                _fileService.Delete(result);
                return new SuccessResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<GetBlogsWithPagination>> GetAllBlogAsync(int page, int limit, string langCode)
        {
            try
            {
                var result = await _blogDAL.GetAllBlogAsync(page,limit,langCode);
                if (result == null)
                    return new ErrorDataResult<GetBlogsWithPagination>(message: MessageStatus.NotFoundMessage("Bloq"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<GetBlogsWithPagination>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new ErrorDataResult<GetBlogsWithPagination>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<GetBlogDetailDTO>> GetBlogDetailAsync(int blogId, string langCode)
        {
            try
            {
                var result = await _blogDAL.GetBlogDetailAsync(blogId, langCode);
                if (result == null)
                    return new ErrorDataResult<GetBlogDetailDTO>(message: MessageStatus.NotFoundMessage("Bloq"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<GetBlogDetailDTO>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new ErrorDataResult<GetBlogDetailDTO>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetBlogListDTO>>> GetLatestBlogAsync(string langCode)
        {
            try
            {
                var result = await _blogDAL.GetLatestBlogAsync(langCode);
                if (result == null)
                    return new ErrorDataResult<List<GetBlogListDTO>>(message: MessageStatus.NotFoundMessage("Bloq"), statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<List<GetBlogListDTO>>(data: result, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new ErrorDataResult<List<GetBlogListDTO>>(message: MessageStatus.ExceptionMessage, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> UpdateBlogAsync(UpdateBlogDTO blogDTO)
        {
            try
            {
                var result = await _blogDAL.UpdateBlogAsync(blogDTO);
                if (result == null)
                    return new ErrorResult(message: MessageStatus.NotFoundMessage("Bloq"), statusCode: HttpStatusCode.BadRequest);
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

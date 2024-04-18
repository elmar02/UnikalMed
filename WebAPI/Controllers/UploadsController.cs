using Business.Utilities.Uploader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly IFileService _fileService;

        public UploadsController(IFileService storageService)
        {
            _fileService = storageService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
            var result = await _fileService.UploadFileAsync(formFile);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFiles(IFormFileCollection formFiles)
        {
            var result = await _fileService.UploadFilesAsync(formFiles);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteFile(string fileUrl)
        {
            _fileService.Delete(fileUrl);  
            return Ok();
        }
    }
}

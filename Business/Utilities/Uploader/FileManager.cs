using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Uploader
{
    public class FileManager : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _contextAccessor;

        public FileManager(IWebHostEnvironment env, IHttpContextAccessor contextAccessor)
        {
            _env = env;
            _contextAccessor = contextAccessor;
        }

        public void Delete(string path)
        {
            var fileName = path.Split('/').Last();
            path = $"{_env.WebRootPath}/uploads/{fileName}";
            File.Delete(path);
        }

        public async Task<string> UploadFileAsync(IFormFile formFile)
        {
            var filePath = Path.Combine(_env.WebRootPath, "uploads").ToLower();
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var path = "/uploads/" + Guid.NewGuid().ToString() + formFile.FileName;
            using FileStream fileStream = new(_env.WebRootPath + path, FileMode.Create);
            await formFile.CopyToAsync(fileStream);
            var requestContext = _contextAccessor?.HttpContext?.Request;
            var baseUrl = $"{requestContext?.Scheme}://{requestContext?.Host}";
            return baseUrl + path;
        }

        public async Task<List<string>> UploadFilesAsync(IFormFileCollection formFiles)
        {
            List<string> filesPath = [];
            foreach (var formFile in formFiles)
            {
                filesPath.Add(await UploadFileAsync(formFile));
            }
            return filesPath;
        }
    }
}

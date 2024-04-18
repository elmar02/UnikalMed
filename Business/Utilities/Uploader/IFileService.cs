using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Uploader
{
    public interface IFileService
    {
        void Delete(string path);
        Task<string> UploadFileAsync(IFormFile formFile);
        Task<List<string>> UploadFilesAsync(IFormFileCollection formFiles);
    }
}

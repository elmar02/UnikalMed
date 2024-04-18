using Core.Utilities.Results.Abstract;
using Entities.DTOs.AdvertDTOs;
using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAdvertService
    {
        Task<IResult> CreatePhotoAsync(CreateAdvertDTO advertDTO);
        Task<IDataResult<List<AdvertDTO>>> GetAllPhotoAsync(string langCode);
        Task<IResult> DeletePhotoAsync(int advertId);
        Task<IDataResult<List<UnhiddenAdvertDTO>>> GetAllUnhiddenPhotoAsync(string langCode);
    }
}

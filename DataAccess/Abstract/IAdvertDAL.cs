using Core.DataAccess;
using Entities.Concrete.AdvertEntities;
using Entities.DTOs.AdvertDTOs;
using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAdvertDAL : IRepositoryBase<Advert>
    {
        Task CreatePhotoAsync(CreateAdvertDTO advertDTO);
        Task<List<AdvertDTO>> GetAllPhotoAsync(string langCode);
        Task<List<string>> DeletePhotoAsync(int advertId);
        Task<List<UnhiddenAdvertDTO>> GetAllUnhiddenPhotoAsync(string langCode);

    }
}

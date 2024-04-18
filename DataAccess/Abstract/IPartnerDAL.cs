using Core.DataAccess;
using Entities.Concrete.PartnerEntities;
using Entities.DTOs.PartnerDTOs;
using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPartnerDAL : IRepositoryBase<Partner>
    {
        Task CreatePhotoAsync(string photoUrl);
        Task<List<PartnerDTO>> GetAllPhotoAsync();
        Task<string> DeletePhotoAsync(int partnerId);
    }
}

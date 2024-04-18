using Core.DataAccess;
using Entities.Concrete.ServiceEntities;
using Entities.DTOs.BrandDTOs;
using Entities.DTOs.ServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IServiceDAL : IRepositoryBase<Service>
    {
        Task<bool> CreateServiceAsync(CreateServiceDTO serviceDTO);
        Task<List<GetServiceListDTO>> GetAllServiceAsync(string langCode);
        Task<string> UpdateServiceAsync(UpdateServiceDTO serviceDTO);
        string DeleteService(int serviceId);
    }
}

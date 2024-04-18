using Core.Utilities.Results.Abstract;
using Entities.DTOs.ServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IServiceService
    {
        Task<IResult> CreateServiceAsync(CreateServiceDTO serviceDTO);
        Task<IDataResult<List<GetServiceListDTO>>> GetAllServiceAsync(string langCode);
        Task<IResult> UpdateServiceAsync(UpdateServiceDTO serviceDTO);
        Task<IResult> DeleteServiceAsync(int serviceId);
    }
}

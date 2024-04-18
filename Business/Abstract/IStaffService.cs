using Core.Utilities.Results.Abstract;
using Entities.DTOs.ServiceDTOs;
using Entities.DTOs.StaffDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStaffService
    {
        Task<IResult> CreateStaffAsync(CreateStaffDTO staffDTO);
        Task<IDataResult<List<GetStaffListDTO>>> GetAllStaffAsync(string langCode);
        Task<IResult> UpdateStaffAsync(UpdateStaffDTO staffDTO);
        Task<IResult> DeleteStaffAsync(int staffId);
    }
}

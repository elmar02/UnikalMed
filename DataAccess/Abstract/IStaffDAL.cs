using Core.DataAccess;
using Entities.Concrete.ServiceEntities;
using Entities.Concrete.StaffEntities;
using Entities.DTOs.ServiceDTOs;
using Entities.DTOs.StaffDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStaffDAL : IRepositoryBase<Staff>
    {
        Task<bool> CreateStaffAsync(CreateStaffDTO staffDTO);
        Task<List<GetStaffListDTO>> GetAllStaffAsync(string langCode);
        Task<string> UpdateStaffAsync(UpdateStaffDTO staffDTO);
        string DeleteStaff(int staffId);
    }
}

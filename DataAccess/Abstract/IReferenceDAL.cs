using Core.DataAccess;
using Entities.Concrete.ReferenceEntities;
using Entities.DTOs.ReferenceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IReferenceDAL : IRepositoryBase<Reference>
    {
        Task<bool> CreateReferenceAsync(CreateReferenceDTO referenceDTO);
        Task<List<GetReferenceListDTO>> GetAllReferenceAsync(string langCode);
        Task<string> UpdateReferenceAsync(UpdateReferenceDTO referenceDTO);
        string DeleteReference(int ReferenceId);
    }
}

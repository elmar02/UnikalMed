using Core.Utilities.Results.Abstract;
using Entities.DTOs.ReferenceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReferenceService
    {
        Task<IResult> CreateReferenceAsync(CreateReferenceDTO referenceDTO);
        Task<IDataResult<List<GetReferenceListDTO>>> GetAllReferenceAsync(string langCode);
        Task<IResult> UpdateReferenceAsync(UpdateReferenceDTO referenceDTO);
        Task<IResult> DeleteReferenceAsync(int referenceId);
    }
}

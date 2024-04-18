using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.BrandDTOs;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBrandDAL : IRepositoryBase<Brand>
    {
        Task<bool> CreateBrandAsync(CreateBrandDTO brandDTO);
        Task<GetBrandDTO> GetBrandAsync(int brandId);
        Task<List<GetBrandListDTO>> GetAllBrandAsync();
        Task<List<GetBrandListDTO>> GetAllDeletedBrandAsync();
        bool SoftDeleteBrand(int brandId);
        bool RestoreBrand(int brandId);
        Task<bool> UpdateBrandAsync(UpdateBrandDTO brandDTO);
        bool HardDeleteBrand(int brandId);
    }
}

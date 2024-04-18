using Core.Utilities.Results.Abstract;
using Entities.DTOs.BrandDTOs;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBrandService
    {
        Task<IResult> CreateBrandAsync(CreateBrandDTO createBrandDTO);
        Task<IDataResult<GetBrandDTO>> GetBrandAsync(int brandId);
        Task<IDataResult<List<GetBrandListDTO>>> GetAllBrandAsync();
        Task<IDataResult<List<GetBrandListDTO>>> GetAllDeletedBrandAsync();
        IResult SoftDeleteBrand(int brandId);
        IResult RestoreBrand(int brandId);
        Task<IResult> UpdateProductAsync(UpdateBrandDTO brandDTO);
        IResult HardDeleteBrand(int brandId);
    }
}

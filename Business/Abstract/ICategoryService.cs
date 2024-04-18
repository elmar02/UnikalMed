using Core.Utilities.Results.Abstract;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IResult> CreateCategoryAsync(CreateCategoryDTO categoryDTO);
        Task<IDataResult<GetCategoryDTO>> GetCategoryAsync(int categoryId, string langCode);
        Task<IDataResult<List<GetCategoryListDTO>>> GetAllCategoryAsync(string langCode);
        Task<IDataResult<List<GetCategoryListDTO>>> GetAllDeletedCategoryAsync(string langCode);
        Task<IResult> UpdateCategoryAsync(UpdateCategoryDTO categoryDTO);
        Task<IDataResult<GetCategoryLangDTO>> GetCategoryWithLang(int categoryId);
        IResult SoftDeleteCategory(int categoryId);
        IResult RestoreCategory(int categoryId);
        IResult HardDeleteCategory(int categoryId);
        Task<IDataResult<List<CatalogDTO>>> GetCatalogAsync(string langCode);
    }
}

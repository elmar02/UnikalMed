using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.CategoryEntities;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICategoryDAL : IRepositoryBase<Category>
    {
        Task<IResult> CreateCategoryAsync(CreateCategoryDTO categoryDTO);
        Task<GetCategoryDTO> GetCategoryAsync(int categoryId ,string langCode);
        Task<List<GetCategoryListDTO>> GetAllCategoriesAsync(string langCode);
        IResult UpdateCategory(UpdateCategoryDTO categoryDTO);
        Task<GetCategoryLangDTO> GetCategoryWithLangAsync(int categoryId);
        IResult SoftDeleteCategory(int categoryId);
        IResult RestoreCategory(int categoryId);
        bool HardDeleteCategory(int categoryId);
        Task<List<GetCategoryListDTO>> GetAllDeletedCategoriesAsync(string langCode);
        Task<List<CatalogDTO>> GetCategoryCatalogAsync(string langCode);
    }
}

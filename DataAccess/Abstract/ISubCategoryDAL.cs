using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.CategoryEntities;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISubCategoryDAL : IRepositoryBase<SubCategory>
    {
        Task<IResult> CreateSubCategoryAsync(CreateSubCategoryDTO categoryDTO);
        Task<GetSubCategoryDTO> GetSubCategoryAsync(int subCategoryId, string langCode);
        Task<List<GetSubCategoryDTO>> GetAllSubCategoriesAsync(string langCode);
        IResult UpdateSubCategory(UpdateSubCategoryDTO categoryDTO);
        Task<GetSubCategoryLangDTO> GetSubCategoryWithLangAsync(int subCategoryId, string langCode);
        IResult SoftDeleteSubCategory(int subCategoryId);
        IResult RestoreSubCategory(int subCategoryId);
        bool HardDeleteSubCategory(int subCategoryId);
        Task<List<GetSubCategoryDTO>> GetAllDeletedSubCategoriesAsync(string langCode);
    }
}

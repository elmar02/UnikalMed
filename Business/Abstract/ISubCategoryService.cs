using Core.Utilities.Results.Abstract;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISubCategoryService
    {
        Task<IResult> CreateSubCategoryAsync(CreateSubCategoryDTO categoryDTO);
        Task<IDataResult<GetSubCategoryDTO>> GetSubCategoryAsync(int subCategoryId, string langCode);
        Task<IDataResult<List<GetSubCategoryDTO>>> GetAllSubCategoryAsync(string langCode);
        Task<IDataResult<List<GetSubCategoryDTO>>> GetAllDeletedSubCategoryAsync(string langCode);
        Task<IResult> UpdateSubCategoryAsync(UpdateSubCategoryDTO categoryDTO);
        Task<IDataResult<GetSubCategoryLangDTO>> GetSubCategoryWithLang(int subCategoryId, string langCode);
        IResult SoftDeleteSubCategory(int subCategoryId);
        IResult RestoreSubCategory(int subCategoryId);
        IResult HardDeleteSubCategory(int categoryId);
    }
}

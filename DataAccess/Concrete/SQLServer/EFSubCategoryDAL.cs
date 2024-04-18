using Core.DataAccess.EntityFramework;
using Core.Helpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.PostgresSQL;
using Entities.Concrete.CategoryEntities;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.SubCategoryDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFSubCategoryDAL : EFRepositoryBase<SubCategory, AppDbContext>, ISubCategoryDAL
    {
        public async Task<IResult> CreateSubCategoryAsync(CreateSubCategoryDTO categoryDTO)
        {
            using var context = new AppDbContext();

            var category = context.Categories.FirstOrDefault(x => x.Id == categoryDTO.CategoryId);
            if (category == null) return new ErrorResult(message: "Belə bir kateqoriya mövcud deyil", statusCode: HttpStatusCode.BadRequest);
            var newSubCategory = new SubCategory()
            {
                CategoryId = categoryDTO.CategoryId,
            };
            await AddAsync(newSubCategory);

            var languages = categoryDTO.Languages;
            foreach (var languageDTO in languages)
            {
                var language = context.SubCategoryLanguages.FirstOrDefault(x => x.SubCategoryName == languageDTO.SubCategoryName);
                if (language != null) return new ErrorResult(message: "Belə bir alt kateqoriya mövcuddur", statusCode: HttpStatusCode.BadRequest);
                var newLanguage = new SubCategoryLanguage()
                {
                    SubCategoryName = languageDTO.SubCategoryName,
                    LangCode = languageDTO.LangCode,
                    SeoUrl = languageDTO.SubCategoryName.ConverToSeo(languageDTO.LangCode),
                    SubCategoryId = newSubCategory.Id,
                };
                await context.SubCategoryLanguages.AddAsync(newLanguage);
            }
            await context.SaveChangesAsync();
            return new SuccessResult(statusCode: HttpStatusCode.OK);
        }

        public async Task<List<GetSubCategoryDTO>> GetAllSubCategoriesAsync(string langCode)
        {
            using var context = new AppDbContext();
            var subCategory = await context.SubCategories
                .Include(x => x.Category)
                .ThenInclude(x => x.Languages)
                .Include(x => x.Languages)
                .Where(x => !x.IsDeleted && !x.Category.IsDeleted)
                .Select(x => new GetSubCategoryDTO()
                {
                    Id = x.Id,
                    SubCategoryName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).SubCategoryName,
                    CategoryName = x.Category.Languages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                }).ToListAsync();

            return subCategory;
        }

        public async Task<GetSubCategoryLangDTO> GetSubCategoryWithLangAsync(int subCategoryId, string langCode)
        {
            using var context = new AppDbContext();

            var subCategory = Get(x => x.Id == subCategoryId);
            if (subCategory == null) return null;
            var lanuages = context.SubCategoryLanguages
                .Include(x => x.SubCategory)
                .ThenInclude(x=>x.Category)
                .Where(x => x.SubCategoryId == subCategoryId && !x.SubCategory.IsDeleted && !x.SubCategory.Category.IsDeleted);
            var languageDTOs = await lanuages.Select(x => new SubCategoryLanguageDTO()
            {
                SubCategoryName = x.SubCategoryName,
                LangCode = x.LangCode,
            }).ToListAsync();

            var allCategories = await context.Categories
                .Include(x=>x.Languages)
                .Select(x => new GetCategoryListDTO()
            {
                Id = x.Id,
                CategoryName = x.Languages.FirstOrDefault(x=>x.LangCode == langCode).CategoryName
            }).ToListAsync();

            var subCategoryLangDTO = new GetSubCategoryLangDTO()
            {
                Languages = languageDTOs,
                CategoryId = subCategory.CategoryId,
                Categories = allCategories
            };
            return subCategoryLangDTO;
        }

        public async Task<GetSubCategoryDTO> GetSubCategoryAsync(int subCategoryId, string langCode)
        {
            using var context = new AppDbContext();
            var subCategory = await context.SubCategories
                .Include(x => x.Category)
                .ThenInclude(x=>x.Languages)
                .Include(x => x.Languages)
                .Where(x => !x.IsDeleted && !x.Category.IsDeleted)
                .Select(x => new GetSubCategoryDTO()
                {
                    Id = x.Id,
                    SubCategoryName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).SubCategoryName,
                    CategoryName = x.Category.Languages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                }).FirstOrDefaultAsync(x => x.Id == subCategoryId);

            return subCategory;
        }

        public async Task<List<GetSubCategoryDTO>> GetAllDeletedSubCategoriesAsync(string langCode)
        {
            using var context = new AppDbContext();
            var subCategory = await context.SubCategories
                .Include(x => x.Category)
                .ThenInclude(x => x.Languages)
                .Include(x => x.Languages)
                .Where(x => x.IsDeleted && !x.Category.IsDeleted)
                .Select(x => new GetSubCategoryDTO()
                {
                    Id = x.Id,
                    SubCategoryName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).SubCategoryName,
                    CategoryName = x.Category.Languages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                }).ToListAsync();

            return subCategory;
        }

        public IResult UpdateSubCategory(UpdateSubCategoryDTO categoryDTO)
        {
            using var context = new AppDbContext();
            var subCategory = Get(x => x.Id == categoryDTO.SubCategoryId && !x.IsDeleted);
            var category = context.Categories.FirstOrDefault(x=>x.Id == categoryDTO.CategoryId);
            if (subCategory == null) return new ErrorResult(message: "Belə bir alt kateqoriya mövcud deyil", statusCode: HttpStatusCode.BadRequest);
            if (category == null && categoryDTO.CategoryId != 0) return new ErrorResult(message: "Belə bir kateqoriya mövcud deyil", statusCode: HttpStatusCode.BadRequest);
            var languages = context.SubCategoryLanguages
                .Where(x => x.SubCategoryId == categoryDTO.SubCategoryId).ToList();

            foreach (var item in categoryDTO.Languages)
            {
                var index = languages.FindIndex(x => x.LangCode == item.LangCode);
                if (index == -1) return new ErrorResult(message: "Belə bir dil kodu mövcud deyil", statusCode: HttpStatusCode.BadRequest);
                languages[index].SubCategoryName = item.SubCategoryName ?? languages[index].SubCategoryName;
                languages[index].SeoUrl = item.SubCategoryName == null ? languages[index].SeoUrl : item.SubCategoryName.ConverToSeo(item.LangCode);
            }

            context.SubCategoryLanguages.UpdateRange(languages);
            if(categoryDTO.CategoryId != 0) 
            {
                subCategory.CategoryId = categoryDTO.CategoryId;
                context.SubCategories.Update(subCategory);
            }
            context.SaveChanges();
            return new SuccessResult(statusCode: HttpStatusCode.OK);
        }

        public IResult SoftDeleteSubCategory(int subCategoryId)
        {
            var subCategory = Get(x => x.Id == subCategoryId && !x.IsDeleted);
            if (subCategory == null) return new ErrorResult(message: "Belə bir alt kateqoriya mövcud deyil", statusCode: HttpStatusCode.BadRequest);

            subCategory.IsDeleted = true;
            Update(subCategory);
            return new SuccessResult(HttpStatusCode.OK);
        }

        public IResult RestoreSubCategory(int subCategoryId)
        {
            var subCategory = Get(x => x.Id == subCategoryId && x.IsDeleted);
            if (subCategory == null) return new ErrorResult(message: "Belə bir alt kateqoriya mövcud deyil", statusCode: HttpStatusCode.BadRequest);

            subCategory.IsDeleted = false;
            Update(subCategory);
            return new SuccessResult(HttpStatusCode.OK);
        }

        public bool HardDeleteSubCategory(int subCategoryId)
        {
            var subCategory = Get(x => x.Id == subCategoryId);
            if (subCategory == null) return false;
            Remove(subCategory);
            return true;
        }
    }
}

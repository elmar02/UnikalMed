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
    public class EFCategoryDAL : EFRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        public async Task<IResult> CreateCategoryAsync(CreateCategoryDTO categoryDTO)
        {
            using var context = new AppDbContext();

            var newCategory = new Category();
            await AddAsync(newCategory);

            var languages = categoryDTO.CategoryNames;
            foreach (var languageDTO in languages)
            {
                var language = context.CategoryLanguages.FirstOrDefault(x=>x.CategoryName == languageDTO.CategoryName);
                if (language != null) return new ErrorResult(message: "Belə bir kateqoriya mövcuddur", statusCode: HttpStatusCode.BadRequest);
                var newLanguage = new CategoryLanguage()
                {
                    CategoryName = languageDTO.CategoryName,
                    LangCode = languageDTO.LangCode,
                    SeoUrl = languageDTO.CategoryName.ConverToSeo(languageDTO.LangCode),
                    CategoryId = newCategory.Id,
                };
                await context.CategoryLanguages.AddAsync(newLanguage);
            }
            await context.SaveChangesAsync();
            return new SuccessResult(statusCode: HttpStatusCode.Created);
        }

        public async Task<List<GetCategoryListDTO>> GetAllCategoriesAsync(string langCode)
        {
            using var context = new AppDbContext();
            var categories = await context.Categories
                .Include(x => x.Languages)
                .Where(x => !x.IsDeleted)
                .Select(x => new GetCategoryListDTO()
                {
                    Id = x.Id,
                    CategoryName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                }).ToListAsync();
            return categories;
        }

        public async Task<List<GetCategoryListDTO>> GetAllDeletedCategoriesAsync(string langCode)
        {
            using var context = new AppDbContext();
            var categories = await context.Categories
                .Include(x => x.Languages)
                .Where(x => x.IsDeleted)
                .Select(x => new GetCategoryListDTO()
                {
                    Id = x.Id,
                    CategoryName = x.Languages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                }).ToListAsync();
            return categories;
        }

        public async Task<GetCategoryDTO> GetCategoryAsync(int categoryId, string langCode)
        {
            using var context = new AppDbContext();
            var category = await context.Categories
                .Include(x=>x.SubCategories)
                .Include(x=>x.Languages)
                .Where(x=>!x.IsDeleted)
                .Select(x=>new GetCategoryDTO()
                {
                    Id = x.Id,
                    CategoryName = x.Languages.FirstOrDefault(x=>x.LangCode == langCode).CategoryName,
                    SubCategories = x.SubCategories.Where(x => !x.IsDeleted).Select(y=> new SubCategoryListDTO()
                    {
                        Id= y.Id,
                        SubCategoryName = y.Languages.FirstOrDefault(z=>z.LangCode == langCode).SubCategoryName,
                    }).ToList(),
                }).FirstOrDefaultAsync(x=>x.Id == categoryId);

            return category;
        }

        public async Task<List<CatalogDTO>> GetCategoryCatalogAsync(string langCode)
        {
            using var context = new AppDbContext();
            var categories = await context.Categories
                .Include(x => x.SubCategories)
                .ThenInclude(x => x.Languages)
                .Include(x => x.Languages)
                .Where(x => !x.IsDeleted)
                .Select(x => new CatalogDTO()
                {
                    SeoUrl = x.Languages.FirstOrDefault(y=>y.LangCode == langCode).SeoUrl,
                    CategoryName = x.Languages.FirstOrDefault(y => y.LangCode == langCode).CategoryName,
                    SubCategories = x.SubCategories.Where(y => !y.IsDeleted).Select(y => new SubCategoryCatalogDTO()
                    {
                        SeoUrl = y.Languages.FirstOrDefault(z=>z.LangCode == langCode).SeoUrl,
                        SubCategoryName = y.Languages.FirstOrDefault(z => z.LangCode == langCode).SubCategoryName
                    }).ToList(),
                }).ToListAsync();

            return categories;
        }

        public async Task<GetCategoryLangDTO> GetCategoryWithLangAsync(int categoryId)
        {
            using var context = new AppDbContext();
            var lanuages = context.CategoryLanguages
                .Include(x=>x.Category)
                .Where(x => x.CategoryId == categoryId && !x.Category.IsDeleted);
            if (!lanuages.Any()) return null;
            var languageDTOs = await lanuages.Select(x => new CategoryLanguageDTO()
            {
                CategoryName = x.CategoryName,
                LangCode = x.LangCode,
            }).ToListAsync();
            var getCategoryLangDTO = new GetCategoryLangDTO()
            {
                Languages = languageDTOs
            };
            return getCategoryLangDTO;
        }

        public bool HardDeleteCategory(int categoryId)
        {
            var category = Get(x=>x.Id == categoryId);
            if(category == null) return false;
            Remove(category);
            return true;
        }

        public IResult RestoreCategory(int categoryId)
        {
            var category = Get(x => x.Id == categoryId && !x.IsDeleted);
            if (category == null) return new ErrorResult(message: "Belə bir kateqoriya mövcud deyil", statusCode: HttpStatusCode.BadRequest);

            category.IsDeleted = false;
            Update(category);
            return new SuccessResult(HttpStatusCode.OK);
        }

        public IResult SoftDeleteCategory(int categoryId)
        {
            var category = Get(x => x.Id == categoryId && !x.IsDeleted);
            if (category == null) return new ErrorResult(message: "Belə bir kateqoriya mövcud deyil", statusCode: HttpStatusCode.BadRequest);

            category.IsDeleted = true;
            Update(category);
            return new SuccessResult(HttpStatusCode.OK);
        }

        public IResult UpdateCategory(UpdateCategoryDTO categoryDTO)
        {
            using var context = new AppDbContext();
            var languages = context.CategoryLanguages
                .Include(x=>x.Category)
                .Where(x => x.CategoryId == categoryDTO.CategoryId && !x.Category.IsDeleted).ToList();
            if (languages.Count == 0) return new ErrorResult(message: "Belə bir kateqoriya mövcud deyil", statusCode: HttpStatusCode.BadRequest);

            foreach (var item in categoryDTO.Languages)
            {
                var index = languages.FindIndex(x => x.LangCode == item.LangCode);
                if (index == -1) return new ErrorResult(message: "Belə bir dil kodu mövcud deyil", statusCode: HttpStatusCode.BadRequest);
                languages[index].CategoryName = item.CategoryName ?? languages[index].CategoryName;
                languages[index].SeoUrl = item.CategoryName == null ? languages[index].SeoUrl : languages[index].CategoryName.ConverToSeo(item.LangCode);
            }

            context.CategoryLanguages.UpdateRange(languages);
            context.SaveChanges();
            return new SuccessResult(statusCode: HttpStatusCode.OK);
        }
    }
}

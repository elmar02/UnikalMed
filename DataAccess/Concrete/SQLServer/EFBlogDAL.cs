using Core.DataAccess.EntityFramework;
using Core.Helpers;
using Core.Helpers.Pagination;
using DataAccess.Abstract;
using DataAccess.Concrete.PostgresSQL;
using Entities.Concrete.BlogEntities;
using Entities.Concrete.StaffEntities;
using Entities.DTOs.BlogDTOs;
using Entities.DTOs.UploadDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFBlogDAL : EFRepositoryBase<Blog, AppDbContext>, IBlogDAL
    {
        public async Task<bool> CreateBlogAsync(CreateBlogDTO blogDTO)
        {
            using var context = new AppDbContext();
            var newBlog = new Blog()
            {
                PhotoUrl = blogDTO.PhotoUrl,
            };
            await AddAsync(newBlog);

            var languages = await context.BlogLanguages.ToListAsync();
            foreach (var language in blogDTO.Languages)
            {
                await context.BlogLanguages.AddAsync(new BlogLanguage()
                {
                    LangCode = language.LangCode,
                    Title = language.Title,
                    SubTitle = language.SubTitle,
                    BlogId = newBlog.Id,
                    Content = language.Content,
                    SeoUrl = (language.Title + (language.SubTitle ?? "")).ConverToSeo(language.LangCode)
                });
            }

            await context.SaveChangesAsync();
            return true;
        }

        public string DeleteBlog(int blogId)
        {
            using var context = new AppDbContext();
            var blog = context.Blogs
                .FirstOrDefault(x => x.Id == blogId);
            if (blog == null) return null;
            Remove(blog);
            return blog.PhotoUrl;
        }

        public async Task<GetBlogsWithPagination> GetAllBlogAsync(int page, int limit, string langCode)
        {
            using var context = new AppDbContext();
            var blogs = await context.Blogs
                .Include(x => x.Languages)
                .Select(x => new GetBlogListDTO()
                {
                    PhotoUrl = x.PhotoUrl,
                    Content = x.Languages.FirstOrDefault(y => y.LangCode == langCode).Content,
                    Title = x.Languages.FirstOrDefault(y => y.LangCode == langCode).Title,
                    SubTitle = x.Languages.FirstOrDefault(y => y.LangCode == langCode).SubTitle,
                    BlogId = x.Id,
                    SeoUrl = x.Languages.FirstOrDefault(y=>y.LangCode == langCode).SeoUrl
                }).ToListAsync();
            var pageDTO = blogs.CreatePage(page, limit);
            return new GetBlogsWithPagination()
            {
                Blogs = blogs,
                Page = pageDTO
            };
        }

        public async Task<GetBlogDetailDTO> GetBlogDetailAsync(int blogId, string langCode)
        {
            using var context = new AppDbContext();
            var blog = await context.Blogs
                .Include(x => x.Languages)
                .FirstOrDefaultAsync(x => x.Id == blogId);

            var blogDTO = new GetBlogDetailDTO()
            {
                PhotoUrl = blog.PhotoUrl,
                Content = blog.Languages.FirstOrDefault(y => y.LangCode == langCode).Content,
                Title = blog.Languages.FirstOrDefault(y => y.LangCode == langCode).Title,
                SubTitle = blog.Languages.FirstOrDefault(y => y.LangCode == langCode).SubTitle,
            };
            return blogDTO;
        }

        public async Task<List<GetBlogListDTO>> GetLatestBlogAsync(string langCode)
        {
            using var context = new AppDbContext();
            var blogs = await context.Blogs
                .Include(x => x.Languages)
                .OrderByDescending(x=>x.UpdatedDate)
                .Take(4)
                .Select(x => new GetBlogListDTO()
                {
                    PhotoUrl = x.PhotoUrl,
                    Content = x.Languages.FirstOrDefault(y => y.LangCode == langCode).Content,
                    Title = x.Languages.FirstOrDefault(y => y.LangCode == langCode).Title,
                    SubTitle = x.Languages.FirstOrDefault(y => y.LangCode == langCode).SubTitle,
                    BlogId = x.Id,
                    SeoUrl = x.Languages.FirstOrDefault(y => y.LangCode == langCode).SeoUrl
                }).ToListAsync();

            return blogs;
        }

        public async Task<string> UpdateBlogAsync(UpdateBlogDTO blogDTO)
        {
            using var context = new AppDbContext();
            var blog = await context.Blogs
                .Include(x => x.Languages)
                .FirstOrDefaultAsync(x => x.Id == blogDTO.BlogId);
            if (blog == null) return null;
            foreach (var languageDTO in blogDTO.Languages)
            {
                var index = blog.Languages.FindIndex(x => x.LangCode == languageDTO.LangCode);
                if (index == -1) continue;
                blog.Languages[index].Title = languageDTO.Title ?? blog.Languages[index].Title;
                blog.Languages[index].SubTitle = languageDTO.SubTitle ?? blog.Languages[index].SubTitle;
                blog.Languages[index].Content = languageDTO.Content ?? blog.Languages[index].Content;
                blog.Languages[index].SeoUrl = (blog.Languages[index].Title + (blog.Languages[index].Title ?? "")).ConverToSeo(languageDTO.LangCode);
            }

            blog.PhotoUrl = blogDTO.PhotoUrl ?? blog.PhotoUrl;
            context.Blogs.Update(blog);
            context.BlogLanguages.UpdateRange(blog.Languages);
            await context.SaveChangesAsync();
            return blogDTO.PhotoUrl ?? "";
        }
    }
}

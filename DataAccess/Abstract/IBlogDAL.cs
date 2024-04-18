using Core.DataAccess;
using Entities.Concrete.BlogEntities;
using Entities.Concrete.StaffEntities;
using Entities.DTOs.BlogDTOs;
using Entities.DTOs.StaffDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBlogDAL : IRepositoryBase<Blog>
    {
        Task<bool> CreateBlogAsync(CreateBlogDTO blogDTO);
        Task<GetBlogsWithPagination> GetAllBlogAsync(int page, int limit, string langCode);
        Task<List<GetBlogListDTO>> GetLatestBlogAsync(string langCode);
        Task<GetBlogDetailDTO> GetBlogDetailAsync(int blogId, string langCode);
        Task<string> UpdateBlogAsync(UpdateBlogDTO blogDTO);
        string DeleteBlog(int blogId);
    }
}

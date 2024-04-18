using Core.Utilities.Results.Abstract;
using Entities.DTOs.BlogDTOs;
using Entities.DTOs.StaffDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBlogService
    {
        Task<IResult> CreateBlogAsync(CreateBlogDTO blogDTO);
        Task<IDataResult<GetBlogsWithPagination>> GetAllBlogAsync(int page, int limit, string langCode);
        Task<IDataResult<List<GetBlogListDTO>>> GetLatestBlogAsync(string langCode);
        Task<IDataResult<GetBlogDetailDTO>> GetBlogDetailAsync(int blogId, string langCode);
        Task<IResult> UpdateBlogAsync(UpdateBlogDTO blogDTO);
        Task<IResult> DeleteBlogAsync(int blogId);
    }
}

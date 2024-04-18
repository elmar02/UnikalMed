using Core.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.BlogDTOs
{
    public class GetBlogsWithPagination
    {
        public PageDTO Page { get; set; }
        public List<GetBlogListDTO> Blogs { get; set; }
    }
}

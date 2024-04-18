using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers.Pagination
{
    public static class PageHelper
    {
        public static PageDTO CreatePage<T>(this List<T> data, int page, int limit)
        {
            PageDTO pageDTO = new()
            {
                PageCount = (int)Math.Ceiling((double)data.Count / limit)
            };
            List<T> pageDatas;
            if (page == 0 || page > pageDTO.PageCount) page = 1;
            pageDatas = data.Skip((page - 1) * limit).Take(limit).ToList();
            pageDTO.ActivePage = page;
            data.Clear();
            data.AddRange(pageDatas);
            return pageDTO;
        }
    }
}

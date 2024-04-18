using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.BlogDTOs
{
    public class BlogLangDTO
    {
        public string LangCode { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string Content { get; set; }
    }
}

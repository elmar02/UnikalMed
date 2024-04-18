using Entities.DTOs.StaffDTOs;
using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.BlogDTOs
{
    public class CreateBlogDTO
    {
        public string PhotoUrl { get; set; }
        public List<BlogLangDTO> Languages { get; set; }
    }
}

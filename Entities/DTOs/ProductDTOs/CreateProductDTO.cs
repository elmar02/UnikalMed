using Entities.DTOs.HeaderDTOs;
using Entities.DTOs.UploadDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class CreateProductDTO
    {
        public List<ProductLanguageDTO> Languages { get; set; }
        public int BrandId { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public int? WattValue { get; set; }
        public int? Guarantee { get; set; }
        public int ShootCount { get; set; }
        public List<HeaderDTO> Headers { get; set; }
        public List<string> PhotoUrls { get; set; }
    }
}

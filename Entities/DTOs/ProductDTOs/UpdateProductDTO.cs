using Entities.DTOs.HeaderDTOs;
using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        public int ProductId { get; set; }
        public List<ProductLanguageDTO> Languages { get; set; }
        public int BrandId { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public int? WattValue { get; set; }
        public int? Guarantee { get; set; }
        public int? ShootCount { get; set; }
        public List<GetHeaderLangDTO> Headers { get; set; }
        public List<int> DeletedHeaderIds { get; set; }
        public List<HeaderDTO> NewHeaders { get; set; }
        public List<int> DeletedProductPictureIds { get; set; }
        public List<string> NewPhotoUrls { get; set; }
    }
}

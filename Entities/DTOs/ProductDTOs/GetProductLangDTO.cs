using Entities.DTOs.BrandDTOs;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.HeaderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class GetProductLangDTO
    {
        public List<ProductLanguageDTO> Languages { get; set; }
        public int BrandId { get; set; }
        public List<GetBrandListDTO> AllBrands { get; set; }
        public int SubCategoryId { get; set; }
        public List<SubCategoryListDTO> SubCategories { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public List<GetHeaderLangDTO> Headers { get; set; }
        public List<GetProductPictureDTO> ProductPictures { get; set; }
    }
}

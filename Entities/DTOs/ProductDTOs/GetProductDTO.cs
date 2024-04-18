using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class GetProductDTO
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }
        public List<string> PhotoUrls { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public List<GetProductHeadersDTO> Headers { get; set; }
        public List<ProductSpecificationDTO> ProductSpecifications { get; set; }
    }
}

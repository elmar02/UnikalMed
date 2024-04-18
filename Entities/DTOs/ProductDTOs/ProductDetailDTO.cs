using Entities.DTOs.HeaderDTOs;
using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ProductDetailDTO
    {
        public List<string> PhotoUrls { get; set; }
        public List<ProductSpecificationDetailDTO> MainSpecifications { get; set; }
        public List<ProductSpecificationDetailDTO> OtherSpecifications { get; set; }
        public List<HeaderDetailDTO> Headers { get; set; }
        public string ProductDescription { get; set; }
        public int? WattValue { get; set; }
        public int? Guarantee { get; set; }
        public int? ShootCount { get; set; }
    }
}

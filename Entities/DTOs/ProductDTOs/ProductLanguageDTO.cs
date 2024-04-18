using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ProductLanguageDTO
    {
        public string LangCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public List<ProductSpecificationDTO> Specifications { get; set; }
    }
}

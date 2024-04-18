using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ProductSpecificationDTO
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsMain { get; set; }
    }
}

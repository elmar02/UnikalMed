using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class GetProductListDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }
    }
}

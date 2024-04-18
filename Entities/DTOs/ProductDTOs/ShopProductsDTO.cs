using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ShopProductsDTO
    {
        public string SeoUrl { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }
}

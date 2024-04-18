using Core.Helpers.Pagination;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class FilterCategoryProductDTO
    {
        public List<ShopProductsDTO> Products { get; set; }
        public PageDTO PageDatas { get; set; }
        public List<FilterSpecificationDTO> Specifications { get; set; }
    }
}

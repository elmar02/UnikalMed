using Entities.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class CategoryFilterDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public List<SubCategoryFilterDTO> SubCategories { get; set; }
    }
}

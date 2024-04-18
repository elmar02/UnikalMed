using Entities.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class CatalogDTO
    {
        public string SeoUrl { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategoryCatalogDTO> SubCategories { get; set; }
    }
}

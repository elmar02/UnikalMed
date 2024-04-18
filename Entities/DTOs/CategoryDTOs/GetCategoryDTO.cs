using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategoryListDTO> SubCategories { get; set; }
    }
}

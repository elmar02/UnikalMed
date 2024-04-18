using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO
    {
        public int CategoryId { get; set; }
        public List<CategoryLanguageDTO> Languages { get; set; }
    }
}

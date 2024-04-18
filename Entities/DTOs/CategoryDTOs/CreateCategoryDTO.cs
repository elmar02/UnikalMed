using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class CreateCategoryDTO
    {
        public List<CategoryLanguageDTO> CategoryNames { get; set; }
    }
}

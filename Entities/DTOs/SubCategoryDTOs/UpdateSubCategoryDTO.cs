using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.SubCategoryDTOs
{
    public class UpdateSubCategoryDTO
    {
        public int SubCategoryId { get; set; }
        public List<SubCategoryLanguageDTO> Languages { get; set; }
        public int CategoryId { get; set; }
    }
}

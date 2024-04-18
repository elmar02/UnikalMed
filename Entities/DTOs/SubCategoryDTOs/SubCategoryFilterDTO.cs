using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.SubCategoryDTOs
{
    public class SubCategoryFilterDTO
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int ProductCount { get; set;}
    }
}

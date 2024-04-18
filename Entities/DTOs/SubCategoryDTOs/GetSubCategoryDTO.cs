using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.SubCategoryDTOs
{
    public class GetSubCategoryDTO
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
    }
}

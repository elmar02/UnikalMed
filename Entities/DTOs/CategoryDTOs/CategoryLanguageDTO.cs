using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class CategoryLanguageDTO
    {
        public string CategoryName { get; set; }
        public string LangCode { get; set; }
    }

}

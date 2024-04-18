using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.HeaderDTOs
{
    public class GetHeaderLanguageUpdateDTO
    {
        public string LangCode { get; set; }
        public List<GetHeaderSpecificationLangDTO> Specifications { get; set; }
    }
}

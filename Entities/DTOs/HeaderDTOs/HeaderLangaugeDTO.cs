using Entities.Concrete.HeaderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.HeaderDTOs
{
    public class HeaderLangaugeDTO
    {
        public string LangCode { get; set; }
        public List<HeaderSpecificationDTO> Specifications { get; set; }
    }
}

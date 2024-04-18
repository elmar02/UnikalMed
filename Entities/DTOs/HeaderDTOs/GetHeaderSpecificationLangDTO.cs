using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.HeaderDTOs
{
    public class GetHeaderSpecificationLangDTO
    {
        public int HeaderSpecificationId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

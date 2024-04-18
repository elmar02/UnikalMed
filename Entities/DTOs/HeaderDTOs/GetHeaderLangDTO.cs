using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.HeaderDTOs
{
    public class GetHeaderLangDTO
    {
        public int HeaderId { get; set; }
        public int WoltValue { get; set; }
        public string PhotoUrl { get; set; }
        public List<HeaderLangaugeDTO> Languages { get; set; }
    }
}

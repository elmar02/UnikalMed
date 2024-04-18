using Entities.Concrete.HeaderEntities;
using Entities.Concrete.ProductEntities;
using Entities.DTOs.UploadDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.HeaderDTOs
{
    public class HeaderDTO
    {
        public int WoltValue { get; set; }
        public string PhotoUrl { get; set; }
        public List<HeaderLangaugeDTO> Languages { get; set; }
    }
}

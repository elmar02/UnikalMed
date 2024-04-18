using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.HeaderDTOs
{
    public class HeaderDetailDTO
    {
        public string PhotoUrl { get; set; }
        public int WoltValue { get; set; }
        public List<HeaderSpecificationDetailDTO> HeaderSpecifications { get; set; }
    }
}

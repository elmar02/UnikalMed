using Entities.Concrete.HeaderEntities;
using Entities.DTOs.HeaderDTOs;
using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class GetProductHeadersDTO
    {
        public int WoltValue { get; set; }
        public string PhotoUrl { get; set; }
        public List<HeaderSpecificationDTO> HeaderSpecifications { get; set; }

    }
}

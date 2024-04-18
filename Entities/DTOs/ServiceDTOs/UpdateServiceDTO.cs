using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ServiceDTOs
{
    public class UpdateServiceDTO
    {
        public int ServiceId { get; set; }
        public List<ServiceLangDTO> Languages { get; set; }
        public string PhotoUrl { get; set; }
    }
}

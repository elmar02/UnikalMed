using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ServiceDTOs
{
    public class GetServiceListDTO
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string PhotoUrl { get; set; }
    }
}

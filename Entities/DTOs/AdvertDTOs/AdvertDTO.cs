using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.AdvertDTOs
{
    public class AdvertDTO
    {
        public int? AdvertId { get; set; }
        public bool IsHidden { get; set; }
        public string Link { get; set; }
        public string PhotoUrl { get; set; }
    }
}

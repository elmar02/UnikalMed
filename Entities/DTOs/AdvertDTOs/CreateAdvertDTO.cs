using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.AdvertDTOs
{
    public class CreateAdvertDTO
    {
        public bool IsHidden { get; set; }
        public string Link { get; set; }
        public List<PhotoUrlWithLangDTO> Languages { get; set; }
    }
}

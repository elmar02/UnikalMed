using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.StaffDTOs
{
    public class CreateStaffDTO
    {
        public string PhotoUrl { get; set; }
        public List<StaffLangDTO> Languages { get; set; }
    }
}

using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.StaffDTOs
{
    public class UpdateStaffDTO
    {
        public int StaffId { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public List<StaffLangDTO> Languages { get; set; }
    }
}

using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.StaffDTOs
{
    public class GetStaffListDTO
    {
        public int StaffId { get; set; }
        public string PhotoUrl { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Profession { get; set; }
        public string Content { get; set; }
    }
}

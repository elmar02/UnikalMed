﻿using Entities.DTOs.StaffDTOs;
using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ReferenceDTOs
{
    public class CreateReferenceDTO
    {
        public string PhotoUrl { get; set; }
        public List<ReferenceLangDTO> Languages { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class FilterSpecificationDTO
    {
        public string Key { get; set; }
        public List<FilterValuesDTO> Values { get; set; }
    }
}

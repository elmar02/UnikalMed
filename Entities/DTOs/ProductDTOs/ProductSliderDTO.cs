using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ProductSliderDTO
    {
        public int ProductId { get; set; }
        public string SeoUrl { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string PhotoUrl { get; set; }
        public int? WattValue { get; set; }
        public int? Guarantee { get; set; }
        public int? ShootCount { get; set; }
    }
}

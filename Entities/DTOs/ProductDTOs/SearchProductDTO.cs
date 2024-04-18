using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class SearchProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SeoUrl { get; set; }
    }
}

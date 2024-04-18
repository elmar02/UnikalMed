using Entities.DTOs.UploadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class GetProductPictureDTO
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
    }
}

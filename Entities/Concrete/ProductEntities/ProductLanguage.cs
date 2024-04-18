using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ProductEntities
{
    public class ProductLanguage : BaseEntity, IEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string SeoUrl { get; set; }
        public List<ProductSpecification> Specifications { get; set; }
        public string LangCode { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

using Core.Entities;
using Entities.Common;
using Entities.Concrete.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Brand : BaseEntity, IEntity
    {
        public string BrandName { get; set; }
        public List<Product> Products { get; set; }
    }
}

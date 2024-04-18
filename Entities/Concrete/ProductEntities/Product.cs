using Core.Entities;
using Entities.Common;
using Entities.Concrete.CategoryEntities;
using Entities.Concrete.HeaderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ProductEntities
{
    public class Product : BaseEntity, IEntity
    {
        public List<ProductLanguage> Languages { get; set; }
        public int? WattValue { get; set; }
        public int? Guarantee { get; set; }
        public int? ShootCount { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public List<Header> Headers { get; set; }
        public List<PictureProduct> PictureProducts { get; set; }
    }
}

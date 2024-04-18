using Core.Entities;
using Entities.Common;
using Entities.Concrete.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.CategoryEntities
{
    public class SubCategory : BaseEntity, IEntity
    {
        public List<SubCategoryLanguage> Languages { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
    }
}

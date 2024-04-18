using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.CategoryEntities
{
    public class Category : BaseEntity, IEntity
    {
        public List<CategoryLanguage> Languages { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}

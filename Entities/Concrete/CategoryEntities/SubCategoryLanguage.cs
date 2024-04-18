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
    public class SubCategoryLanguage : BaseEntity, IEntity
    {
        public string SubCategoryName { get; set; }
        public string SeoUrl { get; set; }
        public string LangCode { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}

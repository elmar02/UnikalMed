using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.CategoryEntities
{
    public class CategoryLanguage : BaseEntity, IEntity
    {
        public string CategoryName { get; set; }
        public string SeoUrl { get; set; }
        public string LangCode { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

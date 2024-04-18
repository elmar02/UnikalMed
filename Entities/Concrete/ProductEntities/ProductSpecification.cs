using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ProductEntities
{
    public class ProductSpecification : BaseEntity, IEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsMain { get; set; }
        public int LanguageId { get; set; }
        public ProductLanguage Language { get; set; }
    }
}

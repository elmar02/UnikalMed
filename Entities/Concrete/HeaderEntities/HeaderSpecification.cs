using Core.Entities;
using Entities.Common;
using Entities.Concrete.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entities.Concrete.HeaderEntities
{
    public class HeaderSpecification : BaseEntity, IEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int LanguageId { get; set; }
        public HeaderLanguage Language { get; set; }
    }
}

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
    public class HeaderLanguage : BaseEntity, IEntity
    {
        public string LangCode { get; set; }
        public int HeaderId { get; set; }
        public Header Header { get; set; }
        public List<HeaderSpecification> Specifications { get; set; }
    }
}

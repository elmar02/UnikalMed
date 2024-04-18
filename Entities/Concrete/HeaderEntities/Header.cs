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
    public class Header : BaseEntity, IEntity
    {
        public int WoltValue { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string PhotoUrl { get; set; }
        public List<HeaderLanguage> Languages { get; set; }
    }
}

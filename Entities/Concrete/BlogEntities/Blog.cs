using Core.Entities;
using Entities.Common;
using Entities.Concrete.StaffEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.BlogEntities
{
    public class Blog : BaseEntity, IEntity
    {
        public string PhotoUrl { get; set; }
        public List<BlogLanguage> Languages { get; set; }
    }
}

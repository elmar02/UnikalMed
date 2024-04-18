using Core.Entities;
using Entities.Common;
using Entities.Concrete.StaffEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ReferenceEntities
{
    public class Reference : BaseEntity, IEntity
    {
        public string PhotoUrl { get; set; }
        public List<ReferenceLanguage> Languages { get; set; }
    }
}

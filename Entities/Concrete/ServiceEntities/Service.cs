using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ServiceEntities
{
    public class Service : BaseEntity, IEntity
    {
        public string PhotoUrl { get; set; }
        public List<ServiceLanguage> Languages { get; set; }
    }
}

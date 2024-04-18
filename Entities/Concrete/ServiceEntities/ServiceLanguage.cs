using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ServiceEntities
{
    public class ServiceLanguage : BaseEntity, IEntity
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public string LangCode { get; set; }
        public string ServiceName { get; set; }
    }
}

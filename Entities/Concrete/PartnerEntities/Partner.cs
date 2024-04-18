using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.PartnerEntities
{
    public class Partner : BaseEntity, IEntity
    {
        public string PhotoUrl { get; set; }
    }
}

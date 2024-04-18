using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.AdvertEntities
{
    public class AdvertLanguage : BaseEntity, IEntity
    {
        public int AdvertId { get; set; }
        public Advert Advert { get; set; }
        public string LangCode { get; set; }
        public string PhotoUrl { get; set; }
    }
}

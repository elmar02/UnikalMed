using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.AdvertEntities
{
    public class Advert : BaseEntity, IEntity
    {
        public bool IsHidden { get; set; }
        public string Link { get; set; }
        public List<AdvertLanguage> Languages { get; set; }
    }
}

using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.StaffEntities
{
    public class Staff : BaseEntity, IEntity
    {
        public string ProfilePhotoUrl { get; set; }
        public List<StaffLanguage> Languages { get; set; }
    }
}

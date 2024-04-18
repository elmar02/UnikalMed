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
    public class ReferenceLanguage : BaseEntity, IEntity
    {
        public int ReferenceId { get; set; }
        public Reference Reference { get; set; }
        public string LangCode { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Address { get; set; }
        public string Content { get; set; }
    }
}

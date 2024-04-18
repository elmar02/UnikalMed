using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.StaffEntities
{
    public class StaffLanguage : BaseEntity, IEntity
    {
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public string LangCode { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Profession { get; set; }
        public string Content { get; set; }
    }
}

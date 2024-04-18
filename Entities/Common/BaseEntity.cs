using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        virtual public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }
        virtual public bool IsDeleted { get; set; }
    }
}

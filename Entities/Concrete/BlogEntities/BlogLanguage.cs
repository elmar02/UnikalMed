using Core.Entities;
using Entities.Common;
using Entities.Concrete.StaffEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.BlogEntities
{
    public class BlogLanguage : BaseEntity, IEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string LangCode { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string SeoUrl { get; set; }
        public string Content { get; set; }
    }
}
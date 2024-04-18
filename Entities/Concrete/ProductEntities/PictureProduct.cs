﻿using Core.Entities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ProductEntities
{
    public class PictureProduct : BaseEntity, IEntity
    {
        public string PhotoUrl { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

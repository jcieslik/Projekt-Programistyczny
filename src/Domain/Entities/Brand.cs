﻿using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Brand : AuditableEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ProductCategory : AuditableEntity
    {
        public string Name { get; set; }
        public ProductCategory ParentCategory { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

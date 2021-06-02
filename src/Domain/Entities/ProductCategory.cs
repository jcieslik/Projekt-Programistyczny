using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ProductCategory : AuditableEntity
    {
        [Required]
        public string Name { get; set; }
        public ProductCategory ParentCategory { get; set; }
        public ICollection<Offer> Offers { get; set; }
        public ICollection<ProductCategory> ChildrenCategories { get; set; }
    }
}

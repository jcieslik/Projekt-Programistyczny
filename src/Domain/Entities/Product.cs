using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public ProductState State { get; set; }

        public Guid CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductRate> Rates { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Offer> Offers { get; set; }
    }
}

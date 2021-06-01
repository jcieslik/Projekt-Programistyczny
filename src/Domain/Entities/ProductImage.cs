using Domain.Common;
using System;

namespace Domain.Entities
{
    public class ProductImage : AuditableEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string Source { get; set; }
        public bool IsMainProductImage { get; set; }
    }
}

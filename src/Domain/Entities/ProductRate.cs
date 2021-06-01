using Domain.Common;
using System;

namespace Domain.Entities
{
    public class ProductRate : AuditableEntity
    {
        public double Value { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid CustomerId { get; set; }
    }
}

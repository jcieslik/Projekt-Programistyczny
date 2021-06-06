using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cart : AuditableEntity
    {
        public Guid CustomerId { get; set; }
        public User Customer { get; set; }
        public ICollection<CartAndOffer> Offers { get; set; }
    }
}

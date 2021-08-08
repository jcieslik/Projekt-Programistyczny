using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cart : AuditableEntity
    {
        public long CustomerId { get; set; }
        public User Customer { get; set; }
        public ICollection<CartOffer> Offers { get; set; }
    }
}

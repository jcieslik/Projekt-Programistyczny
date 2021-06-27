using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class DeliveryMethod : AuditableEntity
    {
        public string Name { get; set; }
        public double BasePrice { get; set; }
        public ICollection<OfferAndDeliveryMethod> Offers { get; set; }
    }
}

using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class OfferAndDeliveryMethod : AuditableEntity
    {
        public Offer Offer { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public double DeliveryFullPrice { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}

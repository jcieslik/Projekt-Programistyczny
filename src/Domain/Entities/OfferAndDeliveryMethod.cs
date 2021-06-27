using Domain.Common;

namespace Domain.Entities
{
    public class OfferAndDeliveryMethod : AuditableEntity
    {
        public Offer Offer { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public double FullPrice { get; set; }
    }
}

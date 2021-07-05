using Domain.Common;

namespace Domain.Entities
{
    public class CartOffer : AuditableEntity
    {
        public Offer Offer { get; set; }
        public Cart Cart { get; set; }
        public int ProductsCount { get; set; }
    }
}

using Domain.Common;

namespace Domain.Entities
{
    public class CartAndOffer : AuditableAndAbleToBeHiddenEntity
    {
        public Offer Offer { get; set; }
        public Cart Cart { get; set; }
    }
}

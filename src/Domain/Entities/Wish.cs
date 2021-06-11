using Domain.Common;

namespace Domain.Entities
{
    public class Wish : AuditableAndAbleToBeHiddenEntity
    {
        public User Customer { get; set; }
        public Offer Offer { get; set; }
    }
}

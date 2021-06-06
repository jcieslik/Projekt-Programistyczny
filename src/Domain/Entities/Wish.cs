using Domain.Common;

namespace Domain.Entities
{
    public class Wish : AuditableEntity
    {
        public User Customer { get; set; }
        public Offer Offer { get; set; }
    }
}

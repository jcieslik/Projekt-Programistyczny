using Domain.Common;

namespace Domain.Entities
{
    public class ProductRate : AuditableAndAbleToBeHiddenEntity
    {
        public double Value { get; set; }
        public Product Product { get; set; }
        public User Customer { get; set; }
    }
}

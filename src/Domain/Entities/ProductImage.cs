using Domain.Common;

namespace Domain.Entities
{
    public class ProductImage : AuditableAndAbleToBeHiddenEntity
    {
        public Product Product { get; set; }
        public string Source { get; set; }
        public bool IsMainProductImage { get; set; }
    }
}

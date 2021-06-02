using Domain.Common;

namespace Domain.Entities
{
    public class ProductImage : AuditableAndAbleToBeHiddenEntity
    {
        public Offer Offer { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsMainProductImage { get; set; }
    }
}

using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ProductRate : AuditableAndAbleToBeHiddenEntity
    {
        [Required]
        public double Value { get; set; }
        public Offer Offer { get; set; }
        public User Customer { get; set; }
    }
}

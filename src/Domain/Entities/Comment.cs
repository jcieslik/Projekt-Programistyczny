using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Comment : AuditableAndAbleToBeHiddenEntity
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public double RateValue { get; set; }
        public Order Order { get; set; }
        public User Customer { get; set; }
        public User Seller { get; set; }
    }
}

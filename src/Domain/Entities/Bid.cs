using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Bid : AuditableAndAbleToBeHiddenEntity
    {
        [Required]
        public double Value { get; set; }
        public User Bidder { get; set; }
        public Offer Offer { get; set; }
    }
}

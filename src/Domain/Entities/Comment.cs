using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Comment : AuditableAndAbleToBeHiddenEntity
    {
        [Required]
        public string Content { get; set; }
        public Offer Offer { get; set; }
        public User Customer { get; set; }
    }
}

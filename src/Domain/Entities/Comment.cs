using Domain.Common;

namespace Domain.Entities
{
    public class Comment : AuditableAndAbleToBeHiddenEntity
    {
        public string Content { get; set; }
        public Offer Offer { get; set; }
        public User Customer { get; set; }
    }
}

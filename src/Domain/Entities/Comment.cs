using Domain.Common;

namespace Domain.Entities
{
    public class Comment : AuditableAndAbleToBeHiddenEntity
    {
        public string Content { get; set; }
        public Product Product { get; set; }
        public User Customer { get; set; }
    }
}

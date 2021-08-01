using Domain.Common;

namespace Domain.Entities
{
    public class MessageUser : AuditableEntity
    {
        public Message Message { get; set; }
        public User Recipient { get; set; }
    }
}

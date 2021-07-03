using Domain.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class MessageTransmission : AuditableAndAbleToBeHiddenEntity
    {
        public User MailboxOwner { get; set; }
        public User Sender { get; set; }
        public User Recipient { get; set; }
        public Message Message { get; set; }
        [Required]
        public MailboxType MailboxType { get; set; }
    }
}

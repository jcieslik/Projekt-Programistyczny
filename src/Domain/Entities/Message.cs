using Domain.Common;
using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Message : AuditableAndAbleToBeHiddenEntity
    {
        public User Sender { get; set; }
        public User Recipient { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public string Content { get; set; }
        
        public DateTime? SendDate { get; set; }

        [Required]
        public MailboxType MailboxType { get; set; }

    }
}

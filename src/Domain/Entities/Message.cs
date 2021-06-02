using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    class Message : AuditableAndAbleToBeHiddenEntity
    {
        public User Sender { get; set; }
        public User Recipient { get; set; }
        
        public string Topic { get; set; }
        public string Content { get; set; }
        
        public DateTime? SendDate { get; set; }

        public MailboxType MailboxType { get; set; }

    }
}

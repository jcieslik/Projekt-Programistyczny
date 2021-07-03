using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Message : AuditableAndAbleToBeHiddenEntity
    {
        [Required]
        public string Topic { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public ICollection<MessageTransmission> Transmissions { get; set; }

    }
}

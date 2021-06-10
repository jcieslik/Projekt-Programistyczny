using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;
using System;

namespace Application.DAL.DTO
{
    public class MessageDTO : EntityDTO, IMapFrom<Message>
    {
        public UserDTO Sender { get; set; }
        public UserDTO Recipient { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public MailboxType MailboxType { get; set; }
    }
}

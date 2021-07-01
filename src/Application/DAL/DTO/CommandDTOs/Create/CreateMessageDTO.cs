using System;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateMessageDTO
    {
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public int MailboxType { get; set; }
    }
}

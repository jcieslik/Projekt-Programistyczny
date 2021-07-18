using System;
using System.Collections.Generic;

namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateMessageDTO
    {
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public IEnumerable<long> RecipientsIds { get; set; }
        public long SenderId { get; set; }
    }
}

using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;
using System;

namespace Application.DAL.DTO
{
    public class BaseMessageDTO : EntityDTO, IMapFrom<Message>
    {
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
    }
}

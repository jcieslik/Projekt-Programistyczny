using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;

namespace Application.DAL.DTO
{
    public class MessageDTO : EntityDTO, IMapFrom<Message>
    {
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public MailboxType MailboxType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageDTO>()
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Sender.Id))
                .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.Recipient.Id));
        }
    }
}

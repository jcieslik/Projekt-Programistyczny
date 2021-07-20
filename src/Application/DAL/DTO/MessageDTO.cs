using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.DAL.DTO
{
    public class MessageDTO : EntityDTO, IMapFrom<Message>
    {
        public long SenderId { get; set; }
        public string Sender { get; set; }
        public IEnumerable<long> RecipientIds { get; set; }
        public IEnumerable<string> Recipients { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public MailboxType MailboxType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MessageTransmission, MessageDTO>()
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Message.Sender.Id))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Message.Sender.Name))
                .ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => src.Message.Recipients.Select(x => x.Recipient.Name)))
                .ForMember(dest => dest.RecipientIds, opt => opt.MapFrom(src => src.Message.Recipients.Select(x => x.Recipient.Id)))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Message.Sender.Name))
                .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Message.Topic))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Message.Content))
                .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => src.Message.SendDate))
                .ForMember(dest => dest.MailboxType, opt => opt.MapFrom(src => src.MailboxType));
        }
    }
}

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
        public IEnumerable<KeyValuePair<long, string>> Recipients { get; set; }
        public string RecipientsString { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public MailboxType MailboxType { get; set; }
        public bool IsRead { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MessageTransmission, MessageDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Message.Id))
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Message.Sender.Id))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Message.Sender.Name))
                .ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => src.Message.Recipients.Select(x => new KeyValuePair<long, string>(x.Recipient.Id, x.Recipient.Username))))
                .ForMember(dest => dest.RecipientsString, opt => opt.MapFrom(src => string.Join(",", src.Message.Recipients.Select(x => x.Recipient.Name).ToArray())))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Message.Sender.Name))
                .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Message.Topic))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Message.Content))
                .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => src.Message.SendDate))
                .ForMember(dest => dest.MailboxType, opt => opt.MapFrom(src => src.MailboxType));
        }
    }
}

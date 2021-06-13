using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.DAL.DTO
{
    public class CommentDTO : EntityDTO, IMapFrom<Comment>
    {
        public string Content { get; set; }
        public Guid OfferId { get; set; }
        public Guid CustomerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.Offer.Id));
        }
    }
}

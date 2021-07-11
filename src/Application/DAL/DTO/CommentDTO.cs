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
        public double RateValue { get; set; }
        public UserDTO Customer { get; set; }
        public long OfferId { get; set; }
        public string OfferTitle { get; set; }
        public long SellerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Seller.Id))
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.Offer.Id))
                .ForMember(dest => dest.OfferTitle, opt => opt.MapFrom(src => src.Offer.Title));
        }
    }
}

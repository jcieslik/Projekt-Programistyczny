using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.DAL.DTO
{
    public class BidDTO : EntityDTO, IMapFrom<Bid>
    {
        public double Value { get; set; }
        public Guid BidderId { get; set; }
        public Guid OfferId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Bid, BidDTO>()
                .ForMember(dest => dest.BidderId, opt => opt.MapFrom(src => src.Bidder.Id))
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.Offer.Id));
        }
    }
}

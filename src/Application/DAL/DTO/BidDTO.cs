using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class BidDTO : EntityDTO, IMapFrom<Bid>
    {
        public double Value { get; set; }
        public long BidderId { get; set; }
        public string BidderUsername { get; set; }
        public long OfferId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Bid, BidDTO>()
                .ForMember(dest => dest.BidderId, opt => opt.MapFrom(src => src.Bidder.Id))
                .ForMember(dest => dest.BidderUsername, opt => opt.MapFrom(src => src.Bidder.Username))
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.Offer.Id));
        }
    }
}

using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class WishDTO : EntityDTO, IMapFrom<Wish>
    {
        public long CustomerId { get; set; }
        public long OfferId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Wish, WishDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.Offer.Id));
        }
    }
}

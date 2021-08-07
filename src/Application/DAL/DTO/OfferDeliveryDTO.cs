using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class OfferDeliveryDTO : EntityDTO, IMapFrom<OfferAndDeliveryMethod>
    {
        public long DeliveryMethodId { get; set; }
        public long OfferId { get; set; }
        public double DeliveryFullPrice { get; set; }
        public string DeliveryMethodName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OfferAndDeliveryMethod, OfferDeliveryDTO>()
                .ForMember(dest => dest.DeliveryMethodId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeliveryMethodName, opt => opt.MapFrom(src => src.DeliveryMethod.Name));
        }
    }
}

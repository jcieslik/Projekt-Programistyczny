using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class DeliveryDTO : EntityDTO, IMapFrom<DeliveryMethod>
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeliveryMethod, DeliveryDTO>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.BasePrice));
        }
    }
}

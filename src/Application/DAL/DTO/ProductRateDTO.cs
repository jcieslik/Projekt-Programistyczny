using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.DAL.DTO
{
    public class ProductRateDTO : EntityDTO, IMapFrom<ProductRate>
    {
        public double Value { get; set; }
        public Guid OfferId { get; set; }
        public Guid CustomerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductRate, ProductRateDTO>()
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.Offer.Id))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id));
        }
    }
}

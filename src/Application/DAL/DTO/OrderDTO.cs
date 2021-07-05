using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;

namespace Application.DAL.DTO
{
    public class OrderDTO : EntityDTO, IMapFrom<Order>
    {
        public long CustomerId { get; set; }
        public long OfferWithDeliveryId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int ProductCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(dest => dest.OfferWithDeliveryId, opt => opt.MapFrom(src => src.OfferWithDelivery.Id));
        }
    }
}

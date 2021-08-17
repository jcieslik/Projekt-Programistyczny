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
        public OrderStatus OrderStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int ProductCount { get; set; }
        public double FullPrice { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationStreet { get; set; }
        public string DestinationPostCode { get; set; }
        public OfferWithBaseDataDTO Offer { get; set; }
        public CommentDTO Comment { get; set; }
        public DeliveryDTO Delivery { get; set; }
        public double? DeliveryFullPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Delivery, opt => opt.MapFrom(src => src.DeliveryMethod))
                .ForMember(dest => dest.Offer, opt => opt.MapFrom(src => src.Offer))
                .ForMember(dest => dest.DeliveryFullPrice, opt => opt.MapFrom(src => src.DeliveryFullPrice));
        }
    }
}

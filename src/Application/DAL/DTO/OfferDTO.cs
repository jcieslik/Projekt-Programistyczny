using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.DAL.DTO
{
    public class OfferDTO : EntityDTO, IMapFrom<Offer>
    {
        public int ProductCount { get; set; }
        public double PriceForOneProduct { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public UserDTO Seller { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public ProductState ProductState { get; set; }
        public OfferState State { get; set; }
        public OfferType OfferType { get; set; }
        public IEnumerable<ProductImageDTO> Images { get; set; }
        public IEnumerable<OfferDeliveryDTO> DeliveryMethods { get; set; }
        public BidDTO BestBid { get; set; }
        public double? MinimalBid { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Offer, OfferDTO>()
                .ForMember(dest => dest.Seller, opt => opt.MapFrom(src => src.Seller))
                .ForMember(dest => dest.DeliveryMethods, opt => opt.MapFrom(src => src.DeliveryMethods))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province.Name))
                .ForMember(dest => dest.BestBid, opt => opt.MapFrom(src => src.Bids.OrderByDescending(x => x.Value).FirstOrDefault()));
        }
    }
}

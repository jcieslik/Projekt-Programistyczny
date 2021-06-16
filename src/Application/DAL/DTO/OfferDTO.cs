using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
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
        public long SellerId { get; set; }
        public long CityId { get; set; }
        public long ProvinceId { get; set; }
        public ProductState ProductState { get; set; }
        public OfferState State { get; set; }
        public OfferType OfferType { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Offer, OfferDTO>()
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Seller.Id))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Brand.Id))
                .ForMember(dest => dest.ProvinceId, opt => opt.MapFrom(src => src.Province.Id))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.City.Id));
        }
    }
}

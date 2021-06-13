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
        public Guid SellerId { get; set; }
        public Guid CityId { get; set; }
        public Guid ProvinceId { get; set; }
        public ProductState ProductState { get; set; }
        public OfferState State { get; set; }
        public OfferType OfferType { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }

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
